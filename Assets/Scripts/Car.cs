using Entities.Spawners;
using Entities.StateMachines;
using Entities.StateMachines.Car.States;
using Functionality.Car;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private RoadSegmentSpawner roadSegmentSpawner;
    [SerializeField] private Vector3 carOffset;
    [SerializeField] private float airborneAngle = 45f;
    
    private CarLogic _carLogic;
    private CarPhysicsSimulation _carPhysicsSimulation;

    private void Awake()
    {
        _carPhysicsSimulation = new CarPhysicsSimulation(
            transform,
            carOffset,
            roadSegmentSpawner.CapturedColliderQueue
        );
        _carLogic = new CarLogic(_carPhysicsSimulation, roadSegmentSpawner.CapturedColliderQueue);
    }

    private void FixedUpdate()
    {
        _carLogic.FixedUpdate();
    }
}