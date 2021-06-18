using System;
using Entities.StateMachines;
using Entities.StateMachines.Car;
using Entities.StateMachines.Car.States;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    private StateMachine _stateMachine;
    private UnityEvent _airborne;
    private UnityEvent _grounded;
    
    private void Start()
    {
        InitializeEvents();
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

    private void OnCollisionEnter()
    {
        _grounded.Invoke();
    }

    private void OnCollisionExit()
    {
        _airborne.Invoke();
    }
    
    private void InitializeEvents()
    {
        _grounded = new UnityEvent();
        _airborne = new UnityEvent();
    }
    
    private void InitializeStateMachine()
    {
        var airborneState = new AirborneCarState();
        var groundedState = new GroundedCarState();
        
        airborneState.AddTransition(groundedState, new CarDecision(_grounded));
        groundedState.AddTransition(airborneState, new CarDecision(_airborne));

        _stateMachine = new StateMachine(groundedState);
    }
}
