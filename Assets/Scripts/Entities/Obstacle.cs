using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Obstacle : MonoBehaviour
    {
        private ObstacleFactory _factory;
        private UnityEvent<Transform, float> _spawnerMovementEvent;

        public void Initialize(ObstacleFactory factory, UnityEvent<Transform, float> spawnerMovementEvent)
        {
            _factory = factory;
            _spawnerMovementEvent = spawnerMovementEvent;
            _spawnerMovementEvent.AddListener(SpawnerMovementEventHandler);
        }

        private void SpawnerMovementEventHandler(Transform roadTransform, float destroyDistance)
        {
            var currentDistance = Vector3.Distance(transform.position, roadTransform.position);

            if (currentDistance >= destroyDistance)
            {
                _factory.Destroy(this);
            }

        }
    }
}
