using System;
using Systems.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private float _airborneTime;

        private readonly CarPhysics _carPhysics;
        private Entities.Car _car;
        
        public AirborneCarState(Entities.Car car, CarPhysics carPhysics)
        {
            _carPhysics = carPhysics;
            _car = car;
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
            _car.CarLandedBufferHandler(_airborneTime);
            _carPhysics.ResetVelocities();
        }
    }
}