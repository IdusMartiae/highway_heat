using Entities.StateMachines;
using Entities.StateMachines.Car.Decisions;
using Entities.StateMachines.Car.States;
using Simulations.Car;
using UnityEngine;

namespace Entities
{
    public class Car : MonoBehaviour
    {
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

        private void OnTriggerEnter(Collider other)
        {
            // TODO: Скорее всего здесь будет уведомление eventSystem вместо этого
            if (other.gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
        
        private void InitializeStateMachine()
        {
            var airborneState = new AirborneCarState(_carPhysics);
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
    }
}
