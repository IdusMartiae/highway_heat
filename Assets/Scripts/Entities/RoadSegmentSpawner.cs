using UnityEngine;

namespace Entities
{
    public class RoadSegmentSpawner : MonoBehaviour
    {
        [SerializeField] private RoadSegment colliderPrefab;
        [SerializeField] private int poolSize = 100;
        [SerializeField] private float colliderDestroyDistance = 10;
        
        private RoadSegmentFactory _roadSegmentFactory;
        private EventSystem _eventSystem;

        public void Awake()
        {
            _eventSystem = new EventSystem();
            _roadSegmentFactory = new RoadSegmentFactory(poolSize, colliderPrefab, _eventSystem.spawnerMovementEvent);
        }

        public void Update()
        {
            var roadSegment = _roadSegmentFactory.Create();
            roadSegment.transform.position = transform.position;
            
            _eventSystem.spawnerMovementEvent.Invoke(transform.parent, colliderDestroyDistance);
        }
    }
}