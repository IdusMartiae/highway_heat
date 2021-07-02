using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Systems.UI.Screens
{
    public class ResultsScreen : BaseScreen
    {
        [SerializeField] private TMP_Text totalScore;
        [SerializeField] private TMP_Text distanceScore;
        [SerializeField] private TMP_Text starsScore;
        [SerializeField] private TMP_Text airTimeScore;
        [SerializeField] private Button retryButton;

        private ScreenSwitch _screenSwitch;
        private ScoreSystem _scoreSystem;

        [Inject]
        private void Initialize(ScreenSwitch screenSwitch, ScoreSystem scoreSystem)
        {
            _screenSwitch = screenSwitch;
            _scoreSystem = scoreSystem;
        }
        
        private void OnEnable()
        {
            SetAllFields();
        }

        private void SetAllFields()
        {
            totalScore.text = _scoreSystem.TotalScore.ToString();
            distanceScore.text = _scoreSystem.DistanceBonus.ToString();
            starsScore.text = _scoreSystem.StarBonus.ToString();
            airTimeScore.text = _scoreSystem.AirborneBonus.ToString();
        }
        
    }
}