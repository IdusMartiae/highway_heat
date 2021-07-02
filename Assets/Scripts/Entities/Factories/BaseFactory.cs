using Configurations;
using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities.Factories
{
    public class BaseFactory : IGameEntityFactory
    {
        protected Pool<GameEntity> pool;
        
        public Transform SpawnerTransform { get; }
        public GameEntityConfiguration GameEntityConfiguration { get; }
        public float DestroyDistance { get; }

        protected BaseFactory(Transform spawnerTransform, GameEntityConfiguration gameEntityConfiguration, float destroyDistance)
        {
            SpawnerTransform = spawnerTransform;
            GameEntityConfiguration = gameEntityConfiguration;
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