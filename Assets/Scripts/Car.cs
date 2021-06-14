using Entities.Spawners;
using Functionality.Car;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private RoadSegmentSpawner roadSegmentSpawner;
    [SerializeField] private Vector3 carOffset;
    [SerializeField] private float airborneAngle = 45f;

    [SerializeField] private float gravity = 5f;
    
    private CarLogic _carLogic;
    private CarPhysicsSimulation _carPhysicsSimulation;

    private void Start()
    {
        _carPhysicsSimulation = new CarPhysicsSimulation(
            transform,
            carOffset,
            roadSegmentSpawner.CapturedColliderQueue,
            gravity
        );
        
        _carLogic = new CarLogic(_carPhysicsSimulation, airborneAngle, roadSegmentSpawner.CapturedColliderQueue);
    }

    private void FixedUpdate()
    {
        _carLogic.FixedUpdate();
    }
}