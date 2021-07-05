using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities.Factories
{
    public class BaseFactory : IGameEntityFactory
    {
        protected Pool<GameEntity> pool;

        public Transform SpawnerTransform { get; }

        protected BaseFactory(Transform spawnerTransform)
        {
            SpawnerTransform = spawnerTransform;
        }

        public virtual GameEntity Create()
        {
            return pool.Pull();
        }

        public void Destroy(GameEntity gameEntity)
        {
            pool.Push(gameEntity);
        }
    }
}