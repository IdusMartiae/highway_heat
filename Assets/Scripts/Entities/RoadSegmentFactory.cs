using UnityEngine;

namespace Entities
{
    public class RoadSegmentFactory : BaseFactory
    {
        public RoadSegmentFactory(
            int poolSize, 
            GameEntity gameEntity,
            Transform spawnerTransform,
            float destroyDistance) : base(spawnerTransform, destroyDistance)
        {
            Pool = new Pool<GameEntity>(poolSize, gameEntity);
        }
    }
}