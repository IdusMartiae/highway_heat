using System;
using Functionality.Car;

namespace Entities.StateMachines
{
    public class Transition
    {
        
        public State transitionState;
        public Func<CarPhysicsSimulation, bool> TransitionCheck;

        public Transition(State state, Func<CarPhysicsSimulation, bool> transitionCheck)
        {
            transitionState = state;
            TransitionCheck = transitionCheck;
        }
       
    }
}