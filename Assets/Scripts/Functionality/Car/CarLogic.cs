using System.Collections.Generic;
using Entities;
using Entities.StateMachines;
using Entities.StateMachines.Car.States;
using Entities.StateMachines.Car.Transitions;

namespace Functionality.Car
{
    public class CarLogic
    {
        private List<GameEntity> _entities;
        private StateMachine _stateMachine;
        private CarPhysicsSimulation _carPhysics;

        private List<State> _states;
        
        public CarLogic(CarPhysicsSimulation carPhysics, List<GameEntity> entities)
        {
            _carPhysics = carPhysics;
            _entities = entities;

            _states = InitializeStates();
            _stateMachine = new StateMachine(_states[0]);
        }

        public void FixedUpdate()
        {
            if (_entities.Count < 2)
            {
                return;
            }

            _stateMachine.Tick();
        }

        private List<State> InitializeStates()
        {
            var list = new List<State>();
            
            list.Add(new AirborneCarState(_carPhysics));
            list.Add(new GroundedCarState(_carPhysics));
            
            list[0].AddTransition(new GroundedTransition(list[1], _carPhysics));
            list[1].AddTransition(new AirborneTransition(list[0], _carPhysics));
            
            return list;
        }
    }
}