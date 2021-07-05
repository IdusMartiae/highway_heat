using System.Collections.Generic;
using Configurations;
using Entities.Factories.Interfaces;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class GameEntity : MonoBehaviour
    {
        [SerializeField] private List<Star> stars;
        
        private GameConfiguration _gameConfiguration;
        private GameEntityConfiguration _gameEntityConfiguration;
        private IGameEntityFactory _factory;

        [Inject]
        public void Construct(GameConfiguration gameConfiguration,
            GameEntityConfiguration gameEntityConfiguration,
            IGameEntityFactory factory)
        {
            _gameConfiguration = gameConfiguration;
            _gameEntityConfiguration = gameEntityConfiguration;
            _factory = factory;
        }
        
        private void Update()
        {
            if (_gameConfiguration.Paused) return;
            
            transform.Translate(Vector3.left * (Time.deltaTime * _gameEntityConfiguration.HorizontalSpeed));
            DestroyOnOutOfBounds(CalculateDistance());
        }
        
        public void ReactivateStars()
        {
            foreach (var star in stars)
            {
                star.gameObject.SetActive(true);
            }
        }

        private float CalculateDistance()
        {
            return Vector3.Distance(transform.position, _factory.SpawnerTransform.position);
        }

        private void DestroyOnOutOfBounds(float currentDistance)
        {
            if (currentDistance >= _gameEntityConfiguration.DestroyDistance)
            {
                _factory.Destroy(this);
            }
        }
    }
}