using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class ObstacleFactory: BaseFactory
    {
        private readonly System.Random _random;
        private readonly List<GameEntity> _obstacles;
        
        public ObstacleFactory(
            List<GameEntity> obstacles,
            Transform spawnerTransform, 
            float destroyDistance) : base(spawnerTransform, destroyDistance)
        {
            Pool = new Pool<GameEntity>(obstacles);
            
            _random = new System.Random();
            _obstacles = obstacles;
        }
        
        public override GameEntity Create()
        {
            var randomObstacleIndex = GetRandomIndex();
            var randomObstacle = _obstacles[randomObstacleIndex];

            var obstacle = Pool.Pull(randomObstacle);
            obstacle.Initialize(this);
            
            return obstacle;
        }
        
        private int GetRandomIndex()
        {
            var index = _random.Next(0, _obstacles.Count);
            
            return index;
        }
    }
}