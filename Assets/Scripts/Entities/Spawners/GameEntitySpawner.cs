using System.Collections.Generic;
using Configurations;
using Entities.Factories;
using UnityEngine;
using Zenject;

namespace Entities.Spawners
{
    public class GameEntitySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameEntity> obstacles;
        
        private GameConfiguration _gameConfiguration;
        private GameEntityConfiguration _gameEntityConfiguration;
        private GameEntityFactory _gameEntityFactory;
        private float _currentInterval;

        public GameEntityFactory GameEntityFactory => _gameEntityFactory ??= new GameEntityFactory(obstacles, transform);
        
        [Inject]
        private void Initialize(GameConfiguration gameConfiguration, GameEntityConfiguration gameEntityConfiguration)
        {
            _gameConfiguration = gameConfiguration;
            _gameEntityConfiguration = gameEntityConfiguration;
        }
        
        private void Update()
        {
            if (_gameConfiguration.Paused) return;
            
            CreateOnTimeInterval();
        }
        
        private void CreateOnTimeInterval()
        {
            _currentInterval += Time.deltaTime;
            
            if (_currentInterval > _gameEntityConfiguration.SpawnInterval)
            {
                CreateObstacle();
            }
        }
        
        private void CreateObstacle()
        {
            _currentInterval = 0;
            var obstacle = GameEntityFactory.Create();
            
            obstacle.transform.position = transform.position;
        }
    }
}