using Simulations.Car;

namespace Entities.StateMachines.Car.States
{
    public class GroundedCarState : State
    {
        private CarPhysics _carPhysics;

        public GroundedCarState(CarPhysics carPhysics)
        {
            _carPhysics = carPhysics;
        }
        
        public override void OnStateEnter()
        {
        }
        
        public override void Tick()
        {
            _carPhysics.UpdateGroundedTransform();
        }

        public override void FixedTick()
        {
            
        }
    }
}