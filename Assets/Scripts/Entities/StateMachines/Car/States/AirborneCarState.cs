using Functionality.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private readonly CarPhysicsSimulation _carPhysics;
        private float _airborneTime;
        private Vector3 _startingPosition;

        public AirborneCarState(CarPhysicsSimulation carPhysics) : base()
        {
            _carPhysics = carPhysics;
        }

        public override void OnStateEnter()
        {
            _airborneTime = 0f;
            _startingPosition = _carPhysics.CarTransform.position;
        }

        public override void Tick()
        {
            _airborneTime += Time.deltaTime;
            _carPhysics.ChangeAirborneTransform(_startingPosition, _airborneTime);
        }

        public override void OnStateExit()
        {
        }
    }
}