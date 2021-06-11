using StateMachines.Interfaces;

namespace StateMachines.Car.Transitions
{
    public class GroundedTransition : ITransitionAdapter
    {
        public bool TransitionCheck()
        {
            return true;
        }
    }
}