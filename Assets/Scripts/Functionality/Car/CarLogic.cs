using System.Collections.Generic;
using Entities;
using Entities.StateMachines;
using Entities.StateMachines.Car.Decisions;
using Entities.StateMachines.Car.States;

namespace Functionality.Car
{
    public class CarLogic
    {
        private readonly CarPhysicsSimulation _carPhysics;
        private readonly float _airborneAngle;
        private readonly List<GameEntity> _entities;
        private StateMachine _stateMachine;

        public CarLogic(CarPhysicsSimulation carPhysics, float airborneAngle, List<GameEntity> entities)
        {
            _carPhysics = carPhysics;
            _airborneAngle = airborneAngle;
            _entities = entities;

            InitializeStateMachine();
        }
        // TODO: Update(){ Tick() }
        // TODO: FixedUpdate(){ FixedTick() }

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
            var airborneState = new AirborneCarState(_carPhysics);
            var groundedState = new GroundedCarState(_carPhysics);

            var airborneDecision = new AirborneStateChangeDecision(_carPhysics.CarTransform, _airborneAngle);
            var groundedDecision = new GroundedStateChangeDecision(_carPhysics.CarTransform, _entities);

            airborneState.AddTransition(groundedState, groundedDecision);
            groundedState.AddTransition(airborneState, airborneDecision);

            _stateMachine = new StateMachine(groundedState);
        }
    }
}