using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameEntity> obstacles;
        [SerializeField] private float destroyDistance = 200;
        [SerializeField] private float spawnInterval = 100f;

        private ObstacleFactory _obstacleFactory;
        private float _currentInterval;

        void Awake()
        {
            _currentInterval = 0f;
            _obstacleFactory = new ObstacleFactory(obstacles, transform, destroyDistance);
        }

        void Update()
        {
            _currentInterval += Time.deltaTime;
            if (_currentInterval > spawnInterval)
            {
                _currentInterval = 0;
                var obstacle = _obstacleFactory.Create().gameObject;
                obstacle.transform.position = transform.position;
            }
        }
    }
}
