using System.Collections.Generic;
using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities
{
    public class GameEntity : MonoBehaviour
    {
        [SerializeField] private List<Star> stars;
        
        // TODO: SET _PAUSED TO TRUE ON EVENT PARAMETER <TRUE>
        private IGameEntityFactory _factory;
        private bool _paused = false;

        private void Update()
        {
            if (_paused) return;
            
            transform.Translate(Vector3.left * (Time.deltaTime * _factory.GameConfiguration.HorizontalSpeed));
            DestroyOnOutOfBounds(CalculateDistance());
        }

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