using Interfaces;
using UnityEngine;

namespace Entities
{
    public class RoadSegmentFactory : IGameObjectFactory
    {
        private Pool<RoadSegment> pool;

        public RoadSegmentFactory(int poolSize, RoadSegment roadSegment, Transform parent)
        {
            pool = new Pool<RoadSegment>(poolSize, parent, roadSegment);
        }

        public GameObject Create()
        {
            var collider = pool.Pull();
            collider.Initialize(this);

            return collider.gameObject;
        }

        public void Destroy(RoadSegment gameObject)
        {
            pool.Push(gameObject);
        }
    }
}