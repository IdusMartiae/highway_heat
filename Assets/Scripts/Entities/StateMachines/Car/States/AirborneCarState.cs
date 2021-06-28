using System;
using Systems.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private float _airborneTime;
        private readonly CarPhysics _carPhysics;
        private event Action<float> CarLanded; 
        
        public AirborneCarState(Entities.Car car, CarPhysics carPhysics)
        {
            CarLanded += car.CarLandedBufferHandler;
            _carPhysics = carPhysics;
        }
        
        public override void OnStateEnter()
        {
            _airborneTime = 0f;
            _carPhysics.InitializeAirborne();
        }
        
        public override void FixedTick()
        {
            _airborneTime += Time.fixedDeltaTime;
            _carPhysics.UpdateAirborneTransform(_airborneTime);
        }

        public override void OnStateExit()
        {
            CarLanded?.Invoke(_airborneTime);
            _carPhysics.ResetVelocities();
        }
    }
}