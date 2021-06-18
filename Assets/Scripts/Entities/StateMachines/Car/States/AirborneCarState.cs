using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private float _airborneTime;

        public override void OnStateEnter()
        {
            _airborneTime = 0f;
        }

        public override void Tick()
        {
            _airborneTime += Time.deltaTime;
        }
    }
}