using System.Collections.Generic;
using UnityEngine;

namespace Entities.Factories
{
    public class ObstacleFactory: BaseFactory
    {
        // TODO: INITIALIZE OBSTACLES WITH GLOBAL HORIZONTAL SPEED 
        public ObstacleFactory(
            List<GameEntity> obstacles,
            Transform spawnerTransform, 
            float destroyDistance) : base(spawnerTransform, destroyDistance)
        {
            pool = new Pool<GameEntity>(obstacles, SpawnerTransform);
        }

        public override GameEntity Create()
        {
            var gameEntity = pool.PullRandom();
            gameEntity.Initialize(this);
            
            return gameEntity;
        }
    }
}