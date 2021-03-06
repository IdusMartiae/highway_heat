using System;
using Entities.Wrappers;

namespace Systems.UI.Screens.Helpers
{
    public class InGameScreenHelper
    {
        public event Action<int> TotalScoreChange = delegate { };
        public event Action<int> TempAirborneScoreChange = delegate { };
        public event Action<int> TempStarScoreChange = delegate { };
        
        private readonly ScoreSystem _scoreSystem;
        private readonly PlayerDataService _playerDataService;
        private TempScoreWrapper _airborneScoreWrapper;
        private TempScoreWrapper _starScoreWrapper;

        public InGameScreenHelper(float fadeDelay,
            float fadeDuration,
            ScoreSystem scoreSystem,
            PlayerDataService playerDataService)
        {
            var scoreLifetime = fadeDelay + fadeDuration;

            _scoreSystem = scoreSystem;
            _playerDataService = playerDataService;
            
            _scoreSystem.TotalScoreChange += HandleTotalScoreChange;
            _scoreSystem.AirborneScoreChange += ChangeAirborneTempScore;
            _scoreSystem.StarScoreChange += ChangeStarTempScore;
            
            InitializeScoreWrappers(scoreLifetime);
        }

        public void OnEnable()
        {
            _scoreSystem.ResetScore();
        }

        public void OnDisable()
        {
            _playerDataService.UpdatePlayerData();
        }
        
        public void Tick()
        {
            UpdateScoreWrappers();
        }

        private void InitializeScoreWrappers(float scoreLifetime)
        {
            _airborneScoreWrapper = new TempScoreWrapper(scoreLifetime);
            _starScoreWrapper = new TempScoreWrapper(scoreLifetime);
        }

        private void HandleTotalScoreChange(int score)
        {
            TotalScoreChange(score);
        }

        // TODO loses refs list of subscribed delegates for some reason || DELETE THIS LATER
        /*private Action<int> GetUINotifier(TempScoreWrapper tempScoreWrapper,
            Action<int> tempScoreChangeNotifier)
        {
            return bonusScore =>
            {
                tempScoreWrapper.Score += bonusScore;
                tempScoreChangeNotifier.Invoke(tempScoreWrapper.Score);
            };
        }*/

        private void ChangeAirborneTempScore(int bonusScore)
        {
            _airborneScoreWrapper.Score += bonusScore;
            TempAirborneScoreChange(_airborneScoreWrapper.Score);
        }

        private void ChangeStarTempScore(int bonusScore)
        {
            _starScoreWrapper.Score += bonusScore;
            TempStarScoreChange(_starScoreWrapper.Score);
        }

        private void UpdateScoreWrappers()
        {
            UpdateScoreWrapper(_airborneScoreWrapper);
            UpdateScoreWrapper(_starScoreWrapper);
        }

        private void UpdateScoreWrapper(TempScoreWrapper tempScoreWrapper)
        {
            if (!tempScoreWrapper.IsScoreAlive())
            {
                _scoreSystem.TotalScore += tempScoreWrapper.FlushDeadScore();
            }
        }
    }
}