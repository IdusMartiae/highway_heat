using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class ObstacleFactory: IGameObjectFactory
    {
        private readonly Pool<Obstacle> _pool;
        private readonly System.Random _random;
        private readonly List<Obstacle> _obstacles;
        private readonly UnityEvent<Transform, float> _spawnerMovementEvent;

        public ObstacleFactory(List<Obstacle> obstacles, UnityEvent<Transform, float> spawnerMovementEvent)
        {
            _pool = new Pool<Obstacle>(obstacles);
            _random = new System.Random();
            _obstacles = obstacles;
            _spawnerMovementEvent = spawnerMovementEvent;
        }
        
        public GameObject Create()
        {
            var randomObstacleIndex = GetRandomItemIndex();
            var randomObstacle = _obstacles[randomObstacleIndex];

            var obstacle = _pool.Pull(randomObstacle);
            obstacle.Initialize(this, _spawnerMovementEvent);
            
            return obstacle.gameObject;
        }

        public void Destroy(Obstacle gameObject)
        {
            _pool.Push(gameObject);
        }

        private int GetRandomItemIndex()
        {
            var index = _random.Next(0, _obstacles.Count);
            return index;
        }
    }
}