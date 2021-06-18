namespace Entities.StateMachines
{
    public class Transition
    {
        public Decision Decision { get; }
        public State State { get; }

        public Transition(State transitionState, Decision decision)
        {
            State = transitionState;
            Decision = decision;
        }
    }
}