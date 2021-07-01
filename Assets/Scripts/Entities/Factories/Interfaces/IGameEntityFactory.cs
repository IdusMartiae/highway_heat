using Configurations;
using UnityEngine;

namespace Entities.Factories.Interfaces
{
    public interface IGameEntityFactory
    {
        Transform SpawnerTransform { get; }
        GameEntityConfiguration GameEntityConfiguration { get; }
        float DestroyDistance { get; }
        GameEntity Create();
        void Destroy(GameEntity gameEntity);
    }
}