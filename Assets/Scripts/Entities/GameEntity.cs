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
            Debug.Log($"This is {name} calling.\n My position is {transform.position.ToString()} \n Parent position {_factory.SpawnerTransform.position.ToString()}");
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