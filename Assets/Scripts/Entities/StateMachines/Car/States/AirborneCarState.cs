using Simulations.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private float _airborneTime;
        private readonly CarPhysics _carPhysics;
        
        public AirborneCarState(CarPhysics carPhysics)
        {
            _carPhysics = carPhysics;
        }
        
        public override void OnStateEnter()
        {
            _airborneTime = 0f;
            _carPhysics.InitializeAirborne();
        }

        public override void Tick()
        {
            _airborneTime += Time.deltaTime;
            _carPhysics.UpdateAirborneTransform(_airborneTime);
        }

        public override void FixedTick()
        {
        }

        public override void OnStateExit()
        {
        }
    }
}