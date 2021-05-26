using Interfaces;
using UnityEngine;

namespace Entities
{
    public class ColliderFactory: IGameObjectFactory
    {
        private ColliderPool _colliderPool;
        
        public ColliderFactory(int poolSize, GameObject gameObject)
        {
            _colliderPool = new ColliderPool(poolSize, gameObject);
        }
        
        public GameObject Create()
        {
           var collider = _colliderPool.Pull();
           
           return collider;
        }

        public void Destroy(GameObject gameObject)
        {
            _colliderPool.Push(gameObject);
        }
    }
}