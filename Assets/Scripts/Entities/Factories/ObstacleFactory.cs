using System.Collections.Generic;
using Configurations;
using UnityEngine;

namespace Entities.Factories
{
    public class ObstacleFactory : BaseFactory
    {
        public ObstacleFactory(
            List<GameEntity> obstacles,
            Transform spawnerTransform,
            GameConfiguration gameConfiguration,
            float destroyDistance) : base(spawnerTransform, gameConfiguration, destroyDistance)
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