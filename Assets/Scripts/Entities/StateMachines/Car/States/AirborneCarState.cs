using UnityEngine;

namespace Entities.StateMachines.Car.States
{
    public class AirborneCarState : State
    {
        private float _airborneTime;
        private readonly Rigidbody _rigidbody;
        private readonly Vector3 _gravityForce;

        public AirborneCarState(Rigidbody rigidbody, Vector3 gravityForce)
        {
            _rigidbody = rigidbody;
            _gravityForce = gravityForce;
        }
        
        public override void OnStateEnter()
        {
            _airborneTime = 0f;
            _rigidbody.useGravity = false;
        }

        public override void Tick()
        {
            _airborneTime += Time.deltaTime;
        }

        public override void FixedTick()
        { 
            _rigidbody.AddForce(_gravityForce * _rigidbody.mass);
        }

        public override void OnStateExit()
        {
            _rigidbody.useGravity = true;
        }
    }
}