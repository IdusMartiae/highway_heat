using Interfaces;
using UnityEngine;

namespace Entities
{
    public class GameEntity : MonoBehaviour
    {
        private IGameEntityFactory _factory;

        private void Update()
        {
            DestroyOnOutOfBounds(CalculateDistance());
        }

        public void Initialize(IGameEntityFactory factory)
        {
            _factory = factory;
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