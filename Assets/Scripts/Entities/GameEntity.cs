using System.Collections.Generic;
using Configurations;
using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities
{
    public class GameEntity : MonoBehaviour
    {
        [SerializeField] private List<Star> stars;
        public GameConfiguration GameConfiguration { get; set; }
        
        private IGameEntityFactory _factory;

        private void Update()
        {
            if (GameConfiguration.Paused) return;
            
            // TODO: cache GameEntityConfiguration ()
            transform.Translate(Vector3.left * (Time.deltaTime * _factory.GameEntityConfiguration.HorizontalSpeed));
            DestroyOnOutOfBounds(CalculateDistance());
        }

        // TODO: Zenject can be used to inject dependencies like that
        public void Initialize(IGameEntityFactory factory)
        {
            _factory = factory;
            ReactivateStars();
        }

        private void ReactivateStars()
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
            if (currentDistance >= _factory.DestroyDistance)
            {
                _factory.Destroy(this);
            }
        }
    }
}