namespace Entities.StateMachines
{
    public abstract class Transition
    {
        public State transitionState;
        
        public abstract bool TransitionCheck();
    }
}