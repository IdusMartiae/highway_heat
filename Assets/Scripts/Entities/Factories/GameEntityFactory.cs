using System.Collections.Generic;
using UnityEngine;

namespace Entities.Factories
{
    public class GameEntityFactory : BaseFactory
    {
        public GameEntityFactory(List<GameEntity> obstacles, Transform spawnerTransform) : base(spawnerTransform)
        {
            pool = new Pool<GameEntity>(obstacles, SpawnerTransform);
        }

        public override GameEntity Create()
        {
            var gameEntity = pool.PullRandom();
            gameEntity.ReactivateStars();
            
            return gameEntity;
        }
        
    }
}