using UnityEngine;

namespace Entities.Factories.Interfaces
{
    public interface IGameEntityFactory
    {
        Transform SpawnerTransform { get; }
        GameConfiguration GameConfiguration { get; }
        float DestroyDistance { get; }
        GameEntity Create();
        void Destroy(GameEntity gameEntity);
    }
}