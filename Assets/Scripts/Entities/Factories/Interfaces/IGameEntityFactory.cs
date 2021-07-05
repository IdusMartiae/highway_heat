using Configurations;
using UnityEngine;

namespace Entities.Factories.Interfaces
{
    public interface IGameEntityFactory
    {
        Transform SpawnerTransform { get; }
        GameEntity Create();
        void Destroy(GameEntity gameEntity);
    }
}