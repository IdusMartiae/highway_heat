using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities
{
    public class GameEntity : MonoBehaviour
    {
        private IGameEntityFactory _factory;

        private void Update()
        {
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
            var stars = GetComponentsInChildren<Star>(true);
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