using System.Collections.Generic;
using Systems.UI;
using Configurations;
using Entities.Factories;
using UnityEngine;

namespace Entities.Spawners
{
    public class GameEntitySpawner : MonoBehaviour
    {
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private GameEntityConfiguration gameEntityConfiguration;
        [SerializeField] private List<GameEntity> obstacles;

        private GameEntityFactory _gameEntityFactory;
        private float _currentInterval;
        
        private void Awake()
        {
            _currentInterval = 0f;
            _gameEntityFactory = new GameEntityFactory(obstacles,
                transform,
                gameEntityConfiguration,
                gameEntityConfiguration.DestroyDistance);
        }

        private void Update()
        {
            if (gameConfiguration.Paused) return;
            
            CreateOnTimeInterval();
        }
        
        private void CreateOnTimeInterval()
        {
            _currentInterval += Time.deltaTime;
            
            if (_currentInterval > gameEntityConfiguration.SpawnInterval)
            {
                CreateObstacle();
            }
        }
        
        private void CreateObstacle()
        {
            _currentInterval = 0;
            var obstacle = _gameEntityFactory.Create();
            
            obstacle.GameConfiguration = gameConfiguration;
            obstacle.transform.position = transform.position;
        }
    }
}