using System;

namespace Systems.UI.Screens.Helpers
{
    public class InGameScreenHelper
    {
        private ScoreSystem _scoreSystem;
        private float _scoreLifetime;
        
        private int _airborneTempBonus;
        private int _starTempBonus;
        
        public event Action<int> AiborneTempScoreChange;
        
        public InGameScreenHelper(float fadeDelay, float fadeDuration, ScoreSystem scoreSystem)
        {
            _scoreLifetime = fadeDelay + fadeDuration;
            _scoreSystem = scoreSystem;
            
            _scoreSystem.TotalScoreChange += HandleTotalScoreChange;
            _scoreSystem.AirborneScoreChange += HandleAirborneBonusChange;
            _scoreSystem.StarScoreChange += HandleStarBonusChange;
        }

        public void Tick()
        {
            
        }
        
        private void HandleTotalScoreChange(int score)
        {
            
        }

        private void HandleAirborneBonusChange(int score)
        {
            
        }

        private void HandleStarBonusChange(int score)
        {
            
        }
    }
}