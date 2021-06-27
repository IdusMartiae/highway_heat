using System.Collections.Generic;
using Entities.Factories;
using UnityEngine;

namespace Entities.Spawners
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private List<GameEntity> obstacles;
        //TODO: move it config too. Probably different one, specific to obstacles
        [SerializeField] private float destroyDistance = 200;
        [SerializeField] private float spawnInterval = 100f;

        private ObstacleFactory _obstacleFactory;
        private float _currentInterval;

        private void Awake()
        {
            _currentInterval = 0f;
            _obstacleFactory = new ObstacleFactory(obstacles, transform, gameConfiguration, destroyDistance);
        }

        private void Update()
        {
            CreateOnTimeInterval();
        }
        
        private void CreateOnTimeInterval()
        {
            UpdateCurrentTimeInterval();
            
            if (_currentInterval > spawnInterval)
            {
                CreateObstacle();
            }
        }

        // TODO: method for one line of code is kinda useless
        private void UpdateCurrentTimeInterval()
        {
            _currentInterval += Time.deltaTime;
        }

        private void CreateObstacle()
        {
            _currentInterval = 0;
            var obstacle = _obstacleFactory.Create();
            
            obstacle.transform.position = transform.position;
        }
    }
}