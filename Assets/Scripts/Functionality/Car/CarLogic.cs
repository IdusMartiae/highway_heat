using System.Collections.Generic;
using Entities;
using Entities.StateMachines;
using Entities.StateMachines.Car.States;

namespace Functionality.Car
{
    public class CarLogic
    {
        private List<GameEntity> _entities;
        private StateMachine _stateMachine;
        private CarPhysicsSimulation _carPhysics;


        public CarLogic(CarPhysicsSimulation carPhysics, List<GameEntity> entities)
        {
            _carPhysics = carPhysics;
            _entities = entities;
            
            InitializeStateMachine();
        }

        public void FixedUpdate()
        {
            if (_entities.Count < 2)
            {
                return;
            }

            _stateMachine.Tick();
        }

        private void InitializeStateMachine()
        {
            var airborne = new AirborneCarState(_carPhysics);
            var grounded = new GroundedCarState(_carPhysics);

            airborne.AddTransition(grounded, CheckIfAirborne);
            grounded.AddTransition(airborne, CheckIfGrounded);

            _stateMachine = new StateMachine(grounded);
        }

        private bool CheckIfAirborne(CarPhysicsSimulation carPhysicsSimulation)
        {
            return true;
        }

        private bool CheckIfGrounded(CarPhysicsSimulation carPhysicsSimulation)
        {
            return true;
        }
    }
}