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

        public abstract void OnStateEnter();

        public abstract void Tick();

        public abstract void OnStateExit();

        public State CheckTransitions()
        {
            foreach (var transition in _transitions.Where(transition => transition.decision.DoDecide()))
            {
                return transition.state;
            }

            return this;
        }

        public void AddTransition(State state, Decision decision)
        {
            _transitions.Add(new Transition(state, decision));
        }
    }
}