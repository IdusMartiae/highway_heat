using Entities;
using UnityEngine;

namespace Spawners
{
    public class RoadColliderSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject colliderPrefab;
        [SerializeField] private int poolSize = 100;
        
        private ColliderFactory _colliderFactory;

        public void Awake()
        {
            _colliderFactory = new ColliderFactory(poolSize, colliderPrefab);
        }

        public void Update()
        {
            Instantiate(_colliderFactory.Create(), transform.position, Quaternion.Euler(0, 90, 0));
        }
        
        //TO DO: Figure out how to push GameObjects back in pool after they left screen bounds.
    }
}