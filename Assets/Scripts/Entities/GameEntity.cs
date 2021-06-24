using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities
{
    public class GameEntity : MonoBehaviour
    {
        // TODO: ADD SEPARATE OBSTACLE SCRIPT THAT DETECTS TRIGGERS, SO CAR IS DESTROYED ON IMPACT
        private IGameEntityFactory _factory;
        public float Speed { get; set; }

        private void Update()
        {
            transform.Translate(Vector3.left * (Time.deltaTime * Speed));
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