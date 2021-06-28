using System;
using Systems.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private float _airborneTime;

        private readonly CarPhysics _carPhysics;
        // private Entities.Car _car;

        // TODO: remove event and invoke method directly
        private event Action<float> CarLanded;

        public AirborneCarState(Entities.Car car, CarPhysics carPhysics)
        {
            // _car = car;
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
            // _car.CarLandedBufferHandler(_airborneTime);
            CarLanded?.Invoke(_airborneTime);
            _carPhysics.ResetVelocities();
        }
    }
}