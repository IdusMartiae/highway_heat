using Entities;
using UnityEngine;

namespace Interfaces
{
    public interface IGameEntityFactory
    {
        Transform SpawnerTransform { get; set; }
        float DestroyDistance { get; set; }

        public GameEntity Create();

        public void Destroy(GameEntity gameEntity);
    }
}