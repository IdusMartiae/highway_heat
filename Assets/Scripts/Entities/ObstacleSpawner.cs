using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> obstacles;
        [SerializeField] private float obstacleDestroyDistance = 200;
        [SerializeField] private float spawnInterval = 100f;

        private ObstacleFactory _obstacleFactory;
        private EventSystem _eventSystem;
        private float _currentInterval;

        void Awake()
        {
            _currentInterval = 0f;
            _eventSystem = new EventSystem();
            _obstacleFactory = new ObstacleFactory(obstacles, _eventSystem.spawnerMovementEvent);
        }

        void Update()
        {
            _currentInterval += Time.deltaTime;
            if (_currentInterval > spawnInterval)
            {
                _currentInterval = 0;
                var obstacle = _obstacleFactory.Create();
                obstacle.transform.position = transform.position;
            }

            _eventSystem.spawnerMovementEvent.Invoke(transform, obstacleDestroyDistance);
        }
    }
}
