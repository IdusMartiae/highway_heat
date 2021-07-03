using System;
using Systems.Car;
using Entities.StateMachines;
using Entities.StateMachines.Car.Decisions;
using Entities.StateMachines.Car.States;
using UnityEngine;

namespace Entities
{
    public class Car : MonoBehaviour
    {
        public event Action<int> PickedUpStar;
        public event Action CarCrashed;
        public event Action<float> CarLanded;

        [SerializeField] private RoadRenderer roadRenderer;
        [SerializeField] private float gravity;
        [SerializeField] private float defaultHorizontalVelocity;
        [SerializeField] private float lerpSpeed;
        [SerializeField] private int anchorPointIndex;

        private StateMachine _stateMachine;
        private CarPhysics _carPhysics;

        private void Start()
        {
            _carPhysics = new CarPhysics(transform,
                gravity,
                roadRenderer,
                anchorPointIndex,
                lerpSpeed,
                defaultHorizontalVelocity);

            InitializeStateMachine();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedTick();
        }

        // TODO: would be better to move this to separate components
        // TODO: ideally this script only holds state machine
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Obstacle>())
            {
                CarCrashed?.Invoke();
                Destroy(gameObject);
            }

            var star = other.gameObject.GetComponent<Star>();
            if (star != null)
            {
                PickedUpStar?.Invoke(star.ScoreValue);
                star.Deinitialize();
            }
        }

        private void InitializeStateMachine()
        {
            var airborneState = new AirborneCarState(this, _carPhysics);
            var groundedState = new GroundedCarState(_carPhysics);

            airborneState.AddTransition(groundedState,
                new ChangeStateToGroundedDecision(transform,
                    _carPhysics,
                    roadRenderer,
                    anchorPointIndex,
                    GetCarHalfLength()));
            groundedState.AddTransition(airborneState,
                new ChangeStateToAirborneDecision(_carPhysics));

            _stateMachine = new StateMachine(groundedState);
        }

        private float GetCarHalfLength()
        {
            var carMesh = gameObject.GetComponentInChildren<MeshFilter>().mesh;
            return carMesh.bounds.size.z / 2;
        }

        public void CarLandedBufferHandler(float airborneTime)
        {
            CarLanded?.Invoke(airborneTime);
        }
    }
}