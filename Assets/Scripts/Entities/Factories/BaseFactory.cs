using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities.Factories
{
    public class BaseFactory : IGameEntityFactory
    {
        public Transform SpawnerTransform { get; }
        public float DestroyDistance { get; }

        protected Pool<GameEntity> Pool;

        protected BaseFactory(Transform spawnerTransform, float destroyDistance)
        {
            SpawnerTransform = spawnerTransform;
            DestroyDistance = destroyDistance;
        }

        public virtual GameEntity Create()
        {
            var gameEntity = Pool.Pull();
            gameEntity.Initialize(this);

            return gameEntity;
        }

        public void Destroy(GameEntity gameEntity)
        {
            Pool.Push(gameEntity);
        }
    }
}