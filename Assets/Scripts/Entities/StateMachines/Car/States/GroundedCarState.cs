using Simulations.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class GroundedCarState : State
    {
        private readonly CarPhysics _carPhysics;

        public GroundedCarState(CarPhysics carPhysics)
        {
            _carPhysics = carPhysics;
        }
        
        public override void OnStateEnter()
        {
            _carPhysics.ResetVelocities();
        }
        
        public override void Tick()
        {
        }

        public override void FixedTick()
        {
            _carPhysics.UpdateGroundedTransform();
        }

        public override void OnStateExit()
        {
        }
    }
}