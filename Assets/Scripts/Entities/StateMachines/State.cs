using System.Collections.Generic;
using System.Linq;

namespace Entities.StateMachines
{
    public abstract class State
    {
        private List<Transition> transitions;

        protected State()
        {
            transitions = new List<Transition>();
        }

        public abstract void OnStateEnter();

        public abstract void Tick();

        public abstract void OnStateExit();

        public State CheckTransitions()
        {
            foreach (var transition in transitions.Where(transition => transition.TransitionCheck()))
            {
                return transition.transitionState;
            }

            return this;
        }

        public void AddTransition(State state, Transition.TransitionCheckDelegate transition)
        {
            transitions.Add(new Transition(state, transition));
        }
    }
}