using UnityEngine;

namespace Entities.Factories
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