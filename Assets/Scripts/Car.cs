using Entities.Spawners;
using Functionality.Car;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private RoadSegmentSpawner roadSegmentSpawner;
    [SerializeField] private Vector3 carOffset;
    
    private CarLogic _carLogic;
    private CarPhysicsSimulation _carPhysicsSimulation;

    private void Awake()
    {
        _carLogic = new CarLogic(roadSegmentSpawner.CapturedColliderQueue);
        _carPhysicsSimulation = new CarPhysicsSimulation(
            transform,
            carOffset,
            roadSegmentSpawner.CapturedColliderQueue
        );
    }

    private void FixedUpdate()
    {
        _carLogic.FixedUpdate();
    }
}