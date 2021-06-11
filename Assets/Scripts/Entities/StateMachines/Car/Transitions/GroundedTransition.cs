using Functionality.Car;

namespace Entities.StateMachines.Car.Transitions
{
    public class GroundedTransition : Transition
    {
        private CarPhysicsSimulation _carPhysics;

        public GroundedTransition(State state, CarPhysicsSimulation carPhysicsSimulation)
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