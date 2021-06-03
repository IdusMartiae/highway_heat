using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class RoadSegmentFactory : IGameObjectFactory
    {
        private readonly Pool<RoadSegment> _pool;
        private readonly UnityEvent<Transform, float> _spawnerMovementEvent;

        public RoadSegmentFactory(int poolSize, RoadSegment roadSegment, UnityEvent<Transform, float> spawnerMovementEvent)
        {
            _pool = new Pool<RoadSegment>(poolSize, roadSegment);
            _spawnerMovementEvent = spawnerMovementEvent;
        }

        public GameObject Create()
        {
            var collider = _pool.Pull();
            collider.Initialize(this, _spawnerMovementEvent);

            return collider.gameObject;
        }

        public void Destroy(RoadSegment gameObject)
        {
            _pool.Push(gameObject);
        }
    }
}