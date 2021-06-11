using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

namespace Functionality.Car
{
    public class CarPhysicsSimulation
    {
        private readonly Transform _carTransform;
        private readonly Vector3 _carOffset;
        private readonly List<GameEntity> _colliderQueue;

        public CarPhysicsSimulation(Transform carTransform, Vector3 carOffset, List<GameEntity> colliderQueue)
        {
            _carTransform = carTransform;
            _carOffset = carOffset;
            _colliderQueue = colliderQueue;
        }

        public void ChangeGroundedTransform()
        {
            var frontPoint = _colliderQueue.Last().transform.position;
            var backPoint = _colliderQueue[0].transform.position;

            var carRotation = GetGroundedRotation(frontPoint, backPoint);

            _carTransform.position = GetGroundedPosition(frontPoint, backPoint, carRotation);
            _carTransform.eulerAngles = carRotation;
        }

        private Vector3 GetGroundedRotation(Vector3 frontPoint, Vector3 backPoint)
        {
            var prevRotation = _carTransform.rotation;

            var deltaX = frontPoint.x - backPoint.x;
            var deltaY = frontPoint.y - backPoint.y;

            var xRotation = -Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;

            return new Vector3(xRotation, prevRotation.y, prevRotation.z);
        }

        private Vector3 GetGroundedPosition(Vector3 frontPoint, Vector3 backPoint, Vector3 rotation)
        {
            var x = (frontPoint.x + backPoint.x) / 2 + _carOffset.x;
            var y = (frontPoint.y + backPoint.y) / 2 + _carOffset.y -
                    _carOffset.z * Mathf.Tan(rotation.x / 180 * Mathf.PI);
            var z = (frontPoint.y + backPoint.y) / 2 + _carOffset.z;

            return new Vector3(x, y, z);
        }

        public void ChangeAirborneTransform()
        {
            
        }

        private Vector3 GetAirbornePosition()
        {


            return new Vector3();
        }

        private Vector3 GetAirborneRotation()
        {
            return new Vector3();
        }
    }
}