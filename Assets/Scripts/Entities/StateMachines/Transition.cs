namespace Entities.StateMachines
{
    public class Transition
    {
        private readonly State state;
        private readonly Decision decision;

        public State State => state;
        public Decision Decision => decision;

        public Transition(State transitionState, Decision transitionDecision)
        {
            decision = transitionDecision;
            state = transitionState;
        }
    }
}