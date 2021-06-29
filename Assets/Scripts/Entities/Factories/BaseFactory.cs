using Configurations;
using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities.Factories
{
    public class BaseFactory : IGameEntityFactory
    {
        protected Pool<GameEntity> pool;
        public Transform SpawnerTransform { get; }
        public GameConfiguration GameConfiguration { get; }
        public float DestroyDistance { get; }

        protected BaseFactory(Transform spawnerTransform, GameConfiguration gameConfiguration, float destroyDistance)
        {
            SpawnerTransform = spawnerTransform;
            GameConfiguration = gameConfiguration;
            DestroyDistance = destroyDistance;
        }

        public virtual GameEntity Create()
        {
            var gameEntity = pool.Pull();
            gameEntity.Initialize(this);

            return gameEntity;
        }

        public void Destroy(GameEntity gameEntity)
        {
            pool.Push(gameEntity);
        }
    }
}