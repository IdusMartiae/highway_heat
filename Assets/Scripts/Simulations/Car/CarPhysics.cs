using UnityEngine;

namespace Simulations.Car
{
    public class CarPhysics
    {
        private Transform _carTransform;
        private float _gravity;
        private float _mass;
        private float _velocityY;
        
        private Transform _frontPoint;
        private Transform _backPoint;
        
        public CarPhysics(Transform carTransform,float gravity, float mass, PathPointScript frontPoint, PathPointScript backPoint)
        {
            _carTransform = carTransform;
            _gravity = gravity;
            _mass = mass;
            
            _frontPoint = frontPoint.transform;
            _backPoint = backPoint.transform;
        }

        public void UpdateGroundedTransform()
        {
            _carTransform.position = GetGroundedPosition();
            _carTransform.eulerAngles = GetGroundedRotation();
        }

        private Vector3 GetGroundedPosition()
        {
            var frontPosition = _frontPoint.position;
            var backPosition = _backPoint.position;
            
            var newX = (frontPosition.x + backPosition.x) / 2;
            var newY = (frontPosition.y + backPosition.y) / 2;
            var newZ = (frontPosition.z + backPosition.z) / 2;
            
            return new Vector3(newX, newY, newZ);
        }

        private Vector3 GetGroundedRotation()
        {
            var frontPosition = _frontPoint.position;
            var backPosition = _backPoint.position;
            
            var deltaY = frontPosition.y - backPosition.y;
            var deltaX = frontPosition.x - backPosition.x;

            var angleX =  - Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
            
            var currentAngles = _carTransform.eulerAngles;
            return new Vector3(angleX, currentAngles.y, currentAngles.z);
        }
    }
}