using Functionality.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private readonly CarPhysicsSimulation _carPhysics;
        private float _airborneTime;
        private Vector3 _startingPosition;
        private Vector2 _velocity;

        public AirborneCarState(CarPhysicsSimulation carPhysics) : base()
        {
            _carPhysics = carPhysics;
        }

        public override void OnStateEnter()
        {
            _airborneTime = 0f;
            _startingPosition = _carPhysics.CarTransform.position;
            
            //
            // pass here road velocity instead of hardcoded values
            _velocity.y = 50 * Mathf.Sin(Mathf.Abs(_carPhysics.CarTransform.eulerAngles.x - 360) / 180 * Mathf.PI);
            _velocity.x = 30;
            //
            //
        }

        public override void FixedTick()
        {
            _airborneTime += Time.deltaTime;
            _carPhysics.ChangeAirborneTransform(_startingPosition,  _velocity, _airborneTime);
        }
    }
}