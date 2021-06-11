using Functionality.Car;

namespace Entities.StateMachines.Car.States
{
    public class GroundedCarState : State
    {
        private readonly CarPhysicsSimulation _carPhysics;
        
        public GroundedCarState(CarPhysicsSimulation carPhysics) : base()
        {
            _carPhysics = carPhysics;
        }
        
        public override void OnStateEnter()
        {
        }

        public override void Tick()
        {
            _carPhysics.ChangeGroundedTransform();
        }

        public override void OnStateExit()
        {
        }
    }
}