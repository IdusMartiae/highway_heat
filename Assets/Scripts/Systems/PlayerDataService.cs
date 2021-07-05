using Configurations;
using UnityEngine;

namespace Systems
{
    public class PlayerDataService
    {
        private readonly ScoreSystem _scoreSystem;
        private readonly GameConfiguration _gameConfiguration;
        
        public PlayerDataService(ScoreSystem scoreSystem, GameConfiguration gameConfiguration)
        {
            _scoreSystem = scoreSystem;
            _gameConfiguration = gameConfiguration;
        }

        public void UpdatePlayerData()
        {
            var playerMoney = GetPlayerMoney();
            var playerHighScore = GetPlayerHighScore();
            
            playerHighScore = _scoreSystem.TotalScore > playerHighScore ? _scoreSystem.TotalScore : playerHighScore;
            
            PlayerPrefs.SetInt(_gameConfiguration.PlayerMoneyKey, _scoreSystem.TotalScore + playerMoney);
            PlayerPrefs.SetInt(_gameConfiguration.PlayerHighScoreKey, playerHighScore);
        }

        public int GetPlayerHighScore()
        {
            return PlayerPrefs.GetInt(_gameConfiguration.PlayerHighScoreKey, 0);
        }

        public int GetPlayerMoney()
        {
            return PlayerPrefs.GetInt(_gameConfiguration.PlayerMoneyKey, 0);
        }
    }
}