using StateMachines.Interfaces;

namespace StateMachines.Car.Transitions
{
    public class AirborneTransition : ITransitionAdapter
    {
        public bool TransitionCheck()
        {
            return true;
        }
    }
}