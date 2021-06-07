using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Factories
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