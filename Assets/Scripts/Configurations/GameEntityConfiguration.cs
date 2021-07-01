using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Game Entity Configuration", fileName = "GameEntityConfiguration")]
    public class GameEntityConfiguration : ScriptableObject
    {
        [SerializeField] private float destroyDistance = 200f;
        [SerializeField] private float spawnInterval = 100f;
        [SerializeField] private float horizontalSpeed = 50f;
        
        public float DestroyDistance => destroyDistance;
        public float SpawnInterval => spawnInterval;
        public float HorizontalSpeed => horizontalSpeed;
    }
}