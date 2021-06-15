using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

namespace Functionality.Car
{
    public class CarPhysicsSimulation
    {
        public Transform CarTransform { get; }
        private readonly float _gravity;
        private readonly Vector3 _carOffset;
        private readonly List<GameEntity> _colliderQueue;

        public CarPhysicsSimulation(Transform carTransform, Vector3 carOffset, List<GameEntity> colliderQueue,
            float gravity)
        {
            CarTransform = carTransform;
            _carOffset = carOffset;
            _colliderQueue = colliderQueue;
            _gravity = gravity;
        }

        public void ChangeGroundedTransform()
        {
            var frontPoint = _colliderQueue.Last().transform.position;
            var backPoint = _colliderQueue[0].transform.position;

            var carRotation = GetGroundedRotation(frontPoint, backPoint);

            CarTransform.position = GetGroundedPosition(frontPoint, backPoint, carRotation);
            CarTransform.eulerAngles = carRotation;
        }

        private Vector3 GetGroundedRotation(Vector3 frontPoint, Vector3 backPoint)
        {
            var prevRotation = CarTransform.eulerAngles;

            var deltaX = frontPoint.x - backPoint.x;
            var deltaY = frontPoint.y - backPoint.y;

            var xRotation = -Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;

            return new Vector3(xRotation, prevRotation.y, prevRotation.z);
        }

        private Vector3 GetGroundedPosition(Vector3 frontPoint, Vector3 backPoint, Vector3 rotation)
        {
            var x = (frontPoint.x + backPoint.x) / 2 + _carOffset.x;
            var y = (frontPoint.y + backPoint.y) / 2 + _carOffset.y -
                    _carOffset.x * Mathf.Tan(rotation.x / 180 * Mathf.PI);
            var z = (frontPoint.z + backPoint.z) / 2 + _carOffset.z;

            return new Vector3(x, y, z);
        }

        public void ChangeAirborneTransform(Vector3 startingPosition, Vector2 velocity,float airborneTime)
        {
            var prevPosition = CarTransform.position;

            CarTransform.position = GetAirbornePosition(startingPosition, velocity, airborneTime);
            CarTransform.eulerAngles = GetAirborneRotation(prevPosition);
        }

        private Vector3 GetAirbornePosition(Vector3 startingPosition, Vector2 velocity, float airborneTime)
        {
            var x = startingPosition.x + velocity.x * airborneTime;
            var y = startingPosition.y + velocity.y * airborneTime - _gravity * airborneTime * airborneTime / 2;
            var z = startingPosition.z;

            return new Vector3(x, y, z);
        }

        private Vector3 GetAirborneRotation(Vector3 prevPosition)
        {
            var currentPosition = CarTransform.position;

            var deltaY = currentPosition.y - prevPosition.y;
            var deltaX = currentPosition.x - prevPosition.x;

            var xRotation = -Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;

            return new Vector3(xRotation, CarTransform.eulerAngles.y, CarTransform.eulerAngles.z);
        }
    }
}