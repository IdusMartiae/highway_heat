using UnityEngine;

namespace Systems.Car
{
    public class CarPhysics
    {
        // TODO: MOVE MARKED WITH * INTO SEPARATE PHYSICS CONFIGURATION OR REPLACE WITH PASSING TO CONSTRUCTOR CAR CLASS
        private readonly Transform _carTransform; // *
        private readonly RoadRenderer _renderer;
        private readonly int _anchorPointIndex; // *
        private float _xAxisAngle;
        private readonly float _lerpSpeed; // *
        
        private float _startingPositionY;
        private float _cachedPositionY;
        private float _xFalseVelocity;
        private readonly float _defaultXFalseVelocity; // *

        public float Gravity { get; }
        public float CachedYVelocity { get; private set; }
        public float CurrentYVelocity { get; set; }
        public float Acceleration { get; private set; }

        public CarPhysics(Transform carTransform,
            float gravity,
            RoadRenderer renderer,
            int anchorPointIndex,
            float lerpSpeed,
            float defaultXVelocity)
        {
            _carTransform = carTransform;
            Gravity = gravity;
            _renderer = renderer;
            _anchorPointIndex = anchorPointIndex;
            _lerpSpeed = lerpSpeed;
            _defaultXFalseVelocity = defaultXVelocity;
        }

        public void UpdateGroundedTransform()
        {
            var normalVector = GetNormalVectorByPointIndex(_anchorPointIndex) * 0.25f;
            var normalVectorAtPoint = normalVector + _renderer.PositionPoints[_anchorPointIndex];
            
            _carTransform.position = Vector3.Lerp(_carTransform.position,
                normalVectorAtPoint,
                Time.fixedDeltaTime * _lerpSpeed); 
            
            _carTransform.rotation = Quaternion.Lerp(_carTransform.rotation,
                GetUpdatedRotationQuaternion(normalVector),
                Time.fixedDeltaTime * _lerpSpeed);
            
            UpdateGroundedVelocityAndAcceleration();
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

        private void UpdateGroundedVelocityAndAcceleration()
        {
            CachedYVelocity = CurrentYVelocity;
            CurrentYVelocity = _renderer.GetPointVerticalVelocity(_anchorPointIndex);
            
            Acceleration = (CurrentYVelocity - CachedYVelocity) / Time.fixedDeltaTime;
        }

        public void UpdateAirborneTransform(float airborneTime)
        {
            UpdateAirbornePosition(airborneTime);
            UpdateAirborneRotation();
            UpdateCurrentVelocity(airborneTime);
        }

        private void UpdateAirbornePosition(float airborneTime)
        {
            var position = _carTransform.position;
            
            _cachedPositionY = position.y;
            _carTransform.position = new Vector3(position.x, GetNewYCoordinate(airborneTime), position.z);
        }
        
        private float GetNewYCoordinate(float airborneTime)
        {
            return _startingPositionY + CachedYVelocity * airborneTime + Gravity * airborneTime * airborneTime / 2;
        }
        
        private void UpdateCurrentVelocity(float airborneTime)
        {
            CurrentYVelocity = CachedYVelocity + Gravity * airborneTime;
        }

        private void UpdateAirborneRotation()
        {
            var deltaX = _xFalseVelocity * Time.fixedDeltaTime;
            var deltaY = _carTransform.position.y - _cachedPositionY;
            
            var x = - Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
            var newRotationQuaternion = Quaternion.Euler(x, 90, 0);

            _carTransform.rotation = newRotationQuaternion;
        }
        
        public void InitializeAirborne()
        {
            CurrentYVelocity = CachedYVelocity;
            _startingPositionY = _carTransform.position.y;
            if (CurrentYVelocity > 5)
            {
                _xFalseVelocity = CurrentYVelocity / - Mathf.Tan(_carTransform.eulerAngles.x * Mathf.Deg2Rad);
            }
            else
            {
                _xFalseVelocity = _defaultXFalseVelocity;
            }
        }

        public void ResetVelocities()
        {
            CurrentYVelocity = 0;
            UpdateGroundedVelocityAndAcceleration();
        }
    }
}