using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Game Configuration", fileName = "GameConfiguration")]
    public class GameConfiguration : ScriptableObject
    {
        [SerializeField] private bool paused;

        public bool Paused
        {
            get => paused;
            set => paused = value;
        }
    }
}