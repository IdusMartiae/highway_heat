using UnityEngine;

namespace Simulations.Car
{
    public class CarPhysics
    {
        private readonly Transform _carTransform;
        private readonly RoadRenderer _renderer;
        private readonly float _gravity;
        private readonly int _anchorPointIndex;
        private float _xAxisAngle;
        private readonly float _lerpSpeed;
        
        private float _startingPositionY;
        private float _cachedPositionY;
        private float _falseXVelocity;
        private float _cachedPositionX;
        
        public float CachedYVelocity { get; set; }
        public float CurrentYVelocity { get; set; }

        public CarPhysics(Transform carTransform, float gravity, RoadRenderer renderer, int anchorPointIndex, float lerpSpeed)
        {
            _carTransform = carTransform;
            _gravity = gravity;
            _renderer = renderer;
            _anchorPointIndex = anchorPointIndex;
            _lerpSpeed = lerpSpeed;
        }

        public void UpdateGroundedTransform()
        {
            var normalVector = GetNormalVectorByPointIndex(_anchorPointIndex) * 0.25f;
            var normalVectorAtPoint = normalVector + _renderer.PositionPoints[_anchorPointIndex];
            
            _carTransform.position = Vector3.Lerp(_carTransform.position, normalVectorAtPoint, Time.deltaTime * _lerpSpeed); 
            _carTransform.rotation = Quaternion.Lerp(_carTransform.rotation, GetUpdatedRotationQuaternion(normalVector), Time.deltaTime * _lerpSpeed);
            
            UpdateGroundedVelocity();
        }

        private Vector3 GetNormalVectorByPointIndex(int index)
        {
            var dx = _renderer.PositionPoints[index].x - _renderer.PositionPoints[index + 1].x;
            var dy = _renderer.PositionPoints[index].y - _renderer.PositionPoints[index + 1].y;

            return new Vector3(-dy, dx, 0).normalized;
        }

        private Quaternion GetUpdatedRotationQuaternion(Vector3 normalVectorScaled)
        {
            var eulerAngles = _carTransform.eulerAngles;
            
            _xAxisAngle = - Vector3.SignedAngle(Vector3.up, normalVectorScaled, Vector3.forward);
            return Quaternion.Euler(_xAxisAngle, eulerAngles.y, eulerAngles.z);
        }

        private void UpdateGroundedVelocity()
        {
            CachedYVelocity = CurrentYVelocity;
            CurrentYVelocity = _renderer.GetPointVerticalVelocity(_anchorPointIndex);
        }

        public void UpdateAirborneTransform(float airborneTime)
        {
            var position = _carTransform.position;
            _cachedPositionY = position.y;
            _carTransform.position = new Vector3(position.x, GetNewYCoordinate(airborneTime), position.z);

            var currentXPosition = _falseXVelocity * airborneTime;
            _carTransform.rotation = GetNewAirborneRotation(_cachedPositionX, currentXPosition, _cachedPositionY, _carTransform.position.y);
            _cachedPositionX = currentXPosition;
            
            UpdateCurrentVelocity(airborneTime);
        }

        private float GetNewYCoordinate(float airborneTime)
        {
            return _startingPositionY + CachedYVelocity * airborneTime + _gravity * airborneTime * airborneTime / 2;
        }
        
        private void UpdateCurrentVelocity(float airborneTime)
        {
            CurrentYVelocity = CachedYVelocity + _gravity * airborneTime;
        }

        private Quaternion GetNewAirborneRotation(float x1, float x2, float y1, float y2)
        {
            var deltaY = y2 - y1;
            var deltaX = Mathf.Abs(x2 - x1) + 0.01f;

            var xAngle = - Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
            return Quaternion.Euler(xAngle, 90, 0);
        }
        
        public void InitializeAirborne()
        {
            _startingPositionY = _carTransform.position.y;
            _cachedPositionX = 0f;
            _falseXVelocity = CurrentYVelocity /  - Mathf.Tan(_carTransform.eulerAngles.x * Mathf.Deg2Rad);
        }

        public void ResetVelocities()
        {
            CurrentYVelocity = 0;
            UpdateGroundedVelocity();
        }
    }
}