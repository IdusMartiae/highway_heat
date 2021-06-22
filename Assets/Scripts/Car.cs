using Entities.StateMachines;
using Entities.StateMachines.Car;
using Entities.StateMachines.Car.Decisions;
using Entities.StateMachines.Car.States;
using Simulations.Car;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    [SerializeField] private RoadRenderer _roadRenderer;
    
    [SerializeField] private PathPointScript frontPoint;
    [SerializeField] private PathPointScript backPoint;
    
    [SerializeField] private float mass;
    [SerializeField] private float gravity;
    [SerializeField] private float speed;

    
    private StateMachine _stateMachine;
    private CarPhysics _carPhysics;
    
    private void Start()
    {
        // _carPhysics = new CarPhysics(transform, gravity, mass, frontPoint, backPoint);
        InitializeStateMachine();
    }

    private void Update()
    {
        var quat = Quaternion.Euler(_roadRenderer.Angle, 90, 0);
            
        transform.position = Vector3.Lerp(transform.position, _roadRenderer.Normal, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, quat, Time.deltaTime * speed);
        // _stateMachine.Tick();
    }

    private void FixedUpdate()
    {
        // _stateMachine.FixedTick();
    }
    
    private void InitializeStateMachine()
    {
        var airborneState = new AirborneCarState(GetComponent<Rigidbody>());
        var groundedState = new GroundedCarState(_carPhysics);
        
        airborneState.AddTransition(groundedState, new ChangeStateToGroundedDecision());
        groundedState.AddTransition(airborneState, new ChangeStateToAirborneDecision());

        _stateMachine = new StateMachine(groundedState);
    }
    
}
