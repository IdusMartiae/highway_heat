using Entities.StateMachines;
using Entities.StateMachines.Car.Decisions;
using Entities.StateMachines.Car.States;
using Simulations.Car;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private RoadRenderer roadRenderer;
    [SerializeField] private float gravity;
    [SerializeField] private float falseHorizontalVelocity;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private int anchorPointIndex;
    [SerializeField] private float groundedPositionThreshold = 0.5f;

    private StateMachine _stateMachine;
    private CarPhysics _carPhysics;
    
    private void Start()
    {
        _carPhysics = new CarPhysics(transform,
            gravity,
            roadRenderer,
            anchorPointIndex,
            lerpSpeed);
        
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
    
    private void InitializeStateMachine()
    {
        var airborneState = new AirborneCarState(_carPhysics);
        var groundedState = new GroundedCarState(_carPhysics);
        
        airborneState.AddTransition(groundedState,
            new ChangeStateToGroundedDecision(transform,
                _carPhysics,
                roadRenderer,
                anchorPointIndex,
                groundedPositionThreshold));
        groundedState.AddTransition(airborneState,
            new ChangeStateToAirborneDecision(_carPhysics));

        _stateMachine = new StateMachine(groundedState);
    }
    
}
