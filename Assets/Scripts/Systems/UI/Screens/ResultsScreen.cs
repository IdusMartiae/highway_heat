using System;
using Entities.Enums;
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

        public override ScreenEnum Type => ScreenEnum.Results;

        [Inject]
        private void Initialize(ScreenSwitch screenSwitch, ScoreSystem scoreSystem)
        {
            _scoreSystem = scoreSystem;
            retryButton.onClick.AddListener(() => screenSwitch.PickScreen(ScreenEnum.MainMenu));
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