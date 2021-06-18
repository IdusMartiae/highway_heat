using System.Collections.Generic;
using System.Linq;

namespace Entities.StateMachines
{
    public abstract class State
    {
        private readonly List<Transition> _transitions;

        protected State()
        {
            _transitions = new List<Transition>();
        }

        public virtual void OnStateEnter()
        {
        }

        public virtual void Tick()
        {
        }

        public virtual void FixedTick()
        {
        }

        public virtual void OnStateExit()
        {
        }

        public State CheckTransitions()
        {
            foreach (var transition in _transitions.Where(transition => transition.Decision.DecisionResult()))
            {
                return transition.State;
            }

            return this;
        }

        public void AddTransition(State state, Decision decision)
        {
            _transitions.Add(new Transition(state, decision));
        }
    }
}