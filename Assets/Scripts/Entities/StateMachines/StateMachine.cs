namespace Entities.StateMachines
{
    public class StateMachine
    {
        private State _currentState;

        public StateMachine(State state)
        {
            _currentState = state;
        }
        
        public void Tick()
        {
            ChangeStateIfNeeded();
            _currentState.Tick();
        }

        public void FixedTick()
        {
            _currentState.FixedTick();
        }
        
        private void ChangeStateIfNeeded()
        {
            var state = _currentState.CheckTransitions();
            
            if (state != _currentState)
            {
                TransitionToState(state);
            }
        }

        public void TransitionToState(State state)
        {
            _currentState.OnStateExit();
            _currentState = state;
            _currentState.OnStateEnter();
        }

    }
}