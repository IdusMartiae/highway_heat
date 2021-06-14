using UnityEngine;

namespace Entities.StateMachines
{
    public class StateMachine
    {
        private State _currentState;

        public StateMachine(State states)
        {
            _currentState = states;
        }
        
        public void Tick()
        {
            _currentState.Tick();
            ChangeStateIfNeeded();
        }
        
        private void ChangeStateIfNeeded()
        {
            var state = _currentState.CheckTransitions();
            
            if (state != _currentState)
            {
                TransitionToState(state);
            }
        }

        private void TransitionToState(State state)
        {
            _currentState.OnStateExit();
            _currentState = state;
            _currentState.OnStateEnter();
        }

    }
}