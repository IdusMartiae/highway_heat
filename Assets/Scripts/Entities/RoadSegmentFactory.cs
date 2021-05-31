using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class RoadSegmentFactory : IGameObjectFactory
    {
        private Pool<RoadSegment> pool;
        private UnityEvent<Transform, float> _spawnerMovementEvent;

        public RoadSegmentFactory(int poolSize, RoadSegment roadSegment, UnityEvent<Transform, float> spawnerMovementEvent)
        {
            pool = new Pool<RoadSegment>(poolSize, roadSegment);
            _spawnerMovementEvent = spawnerMovementEvent;
        }

        public GameObject Create()
        {
            var collider = pool.Pull();
            collider.Initialize(this, _spawnerMovementEvent);

            return collider.gameObject;
        }

        public void Destroy(RoadSegment gameObject)
        {
            pool.Push(gameObject);
        }
    }
}