using Functionality.Car;

namespace Entities.StateMachines.Car.Transitions
{
    public class AirborneTransition : Transition
    {
        private CarPhysicsSimulation _carPhysics;

        public AirborneTransition(State state, CarPhysicsSimulation carPhysicsSimulation)
        {
            transitionState = state;
            _carPhysics = carPhysicsSimulation;
        }

        public override bool TransitionCheck()
        {
            return true;
        }
    }
}