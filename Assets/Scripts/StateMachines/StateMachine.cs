using StateMachines.Car.States;
using StateMachines.Interfaces;

namespace StateMachines
{
    public class StateMachine
    {
        private IStateAdapter _currentState;

        public StateMachine()
        {
            _currentState = new GroundedCarState();
        }

        public void TransitionToState(IStateAdapter state)
        {
            _currentState = state;
        }
    }
}