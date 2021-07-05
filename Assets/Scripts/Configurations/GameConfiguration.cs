using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Game Configuration", fileName = "GameConfiguration")]
    public class GameConfiguration : ScriptableObject
    {
        [SerializeField] private bool paused;
        
        public bool Paused { get; set; }
        public string PlayerMoneyKey { get; } = "PlayerMoney";
        public string PlayerHighScoreKey { get; } = "PlayerHighScore";

        private void OnEnable()
        {
            Paused = paused;
        }
    }
}