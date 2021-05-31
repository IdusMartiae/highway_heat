using Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class RoadSegmentSpawner : MonoBehaviour
    {
        [SerializeField] private RoadSegment colliderPrefab;
        [SerializeField] private int poolSize = 100;
        [SerializeField] private float colliderDestroyDistance = 10;

        private RoadSegmentFactory roadSegmentFactory;
        private UnityEvent<Transform, float> spawnerMovementEvent = new UnityEvent<Transform, float>();

        public void Awake()
        {
            roadSegmentFactory = new RoadSegmentFactory(poolSize, colliderPrefab, spawnerMovementEvent);
        }

        public void Update()
        {
            var roadSegment = roadSegmentFactory.Create();
            roadSegment.transform.position = transform.position;
            
            spawnerMovementEvent.Invoke(transform.parent, colliderDestroyDistance);
        }
    }
}