using Entities.Factories.Interfaces;
using UnityEngine;

namespace Entities.Factories
{
    public class BaseFactory : IGameEntityFactory
    {   
        // TODO: INITIALIZE GAME ENTITY W/ GLOBAL HORIZONTAL SPEED AS IT'S CREATED
        public Transform SpawnerTransform { get; }
        public float DestroyDistance { get; }
        
        protected Pool<GameEntity> pool;

        protected BaseFactory(Transform spawnerTransform, float destroyDistance)
        {
            SpawnerTransform = spawnerTransform;
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