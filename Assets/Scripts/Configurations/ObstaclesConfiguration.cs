using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Obstacles Configuration", fileName = "ObstaclesConfiguration")]
    public class ObstaclesConfiguration : ScriptableObject
    {
        [SerializeField] private float destroyDistance = 200f;
        [SerializeField] private float spawnInterval = 100f;

        public float DestroyDistance => destroyDistance;

        public float SpawnInterval => spawnInterval;
    }
}