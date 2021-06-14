namespace Entities.StateMachines
{
    public class Transition
    {
        public readonly State state;
        public readonly Decision decision;

        public Transition(State transitionState, Decision transitionDecision)
        {
            decision = transitionDecision;
            state = transitionState;
        }
    }
}