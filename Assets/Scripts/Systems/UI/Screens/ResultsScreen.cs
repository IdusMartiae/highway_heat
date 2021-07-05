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
        [SerializeField] private TMP_Text money;
        [SerializeField] private TMP_Text highScore;
        [SerializeField] private Button retryButton;

        private ScoreSystem _scoreSystem;
        private PlayerDataService _playerDataService;

        public override ScreenEnum Type => ScreenEnum.Results;

        [Inject]
        private void Initialize(ScreenSwitch screenSwitch, ScoreSystem scoreSystem, PlayerDataService playerDataService)
        {
            _scoreSystem = scoreSystem;
            _playerDataService = playerDataService;
            retryButton.onClick.AddListener(() => screenSwitch.PickScreen(ScreenEnum.MainMenu));
        }
        
        private void OnEnable()
        {
            InitializeTextFields();
        }

        private void InitializeTextFields()
        {
            totalScore.text = _scoreSystem.TotalScore.ToString();
            distanceScore.text = _scoreSystem.DistanceBonus.ToString();
            starsScore.text = _scoreSystem.StarBonus.ToString();
            airTimeScore.text = _scoreSystem.AirborneBonus.ToString();
            
            money.text = _playerDataService.GetPlayerMoney().ToString();
            highScore.text = _playerDataService.GetPlayerHighScore().ToString();
        }
        
    }
}