using Simulations.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private float _airborneTime;
        private readonly CarPhysics _carPhysics;
        
        public AirborneCarState(CarPhysics carPhysics)
        {
            _carPhysics = carPhysics;
        }
        
        public override void OnStateEnter()
        {
            _airborneTime = 0f;
            _carPhysics.InitializeAirborne();
        }

        public override void Tick()
        {
        }

        public override void FixedTick()
        {
            _airborneTime += Time.fixedDeltaTime;
            _carPhysics.UpdateAirborneTransform(_airborneTime);
        }

        public override void OnStateExit()
        {
            _carPhysics.ResetVelocities();
        }
    }
}