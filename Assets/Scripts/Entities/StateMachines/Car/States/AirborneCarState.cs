using Functionality.Car;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private readonly CarPhysicsSimulation _carPhysics;

        public AirborneCarState(CarPhysicsSimulation carPhysics) : base()
        {
            _carPhysics = carPhysics;
        }

        public override void OnStateEnter()
        {
        }

        public override void Tick()
        {
            _carPhysics.ChangeAirborneTransform();
        }

        public override void OnStateExit()
        {
        }
    }
}