using Simulations.Car;

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
        
        // TODO: don't have to override if not using it
        public override void Tick()
        {
        }

        public override void FixedTick()
        {
            _carPhysics.UpdateGroundedTransform();
        }
        
        // TODO: don't have to override if not using it
        public override void OnStateExit()
        {
        }
    }
}