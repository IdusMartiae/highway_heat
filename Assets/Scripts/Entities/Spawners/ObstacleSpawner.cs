using System.Collections.Generic;
using Configurations;
using Entities.Factories;
using UnityEngine;

namespace Entities.Spawners
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private ObstaclesConfiguration obstaclesConfiguration;
        [SerializeField] private List<GameEntity> obstacles;
        
        private ObstacleFactory _obstacleFactory;
        private float _currentInterval;

        private void Awake()
        {
            _currentInterval = 0f;
            _obstacleFactory = new ObstacleFactory(obstacles,
                transform,
                gameConfiguration,
                obstaclesConfiguration.DestroyDistance);
        }

        private void Update()
        {
            CreateOnTimeInterval();
        }
        
        private void CreateOnTimeInterval()
        {
            _currentInterval += Time.deltaTime;
            
            if (_currentInterval > obstaclesConfiguration.SpawnInterval)
            {
                CreateObstacle();
            }
        }
        
        private void CreateObstacle()
        {
            _currentInterval = 0;
            var obstacle = _obstacleFactory.Create();
            
            obstacle.transform.position = transform.position;
        }
    }
}