using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class ObstacleFactory: BaseFactory
    {
        
        public ObstacleFactory(
            List<GameEntity> obstacles,
            Transform spawnerTransform, 
            float destroyDistance) : base(spawnerTransform, destroyDistance)
        {
            Pool = new Pool<GameEntity>(obstacles);
        }

        public override GameEntity Create()
        {
            var gameEntity = Pool.PullRandom();
            gameEntity.Initialize(this);
            
            return gameEntity;
        }
    }
}