using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class RoadSegment : MonoBehaviour
    {
        private RoadSegmentFactory _factory;
        private UnityEvent<Transform, float> _spawnerMovementEvent;

        public void Initialize(RoadSegmentFactory factory, UnityEvent<Transform,float> spawnerMovementEvent)
        {
            _factory = factory;
            _spawnerMovementEvent = spawnerMovementEvent;
            _spawnerMovementEvent.AddListener(SpawnerMovementEventHandler);
        }
        
        public void SpawnerMovementEventHandler(Transform roadTransform, float destroyDistance)
        {
            float currentDistance = Vector3.Distance(transform.position, roadTransform.position);
            
            if (currentDistance >= destroyDistance)
            {
                _factory.Destroy(this);
            }
            
        }
    }
}