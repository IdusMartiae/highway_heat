using System;
using Systems.UI.Screens.Helpers;
using Configurations;
using DG.Tweening;
using Entities.Enums;
using TMPro;
using UnityEngine;
using Zenject;

namespace Systems.UI.Screens
{
    public class InGameScreen : BaseScreen
    {
        [SerializeField] private TMP_Text totalScoreText;
        [SerializeField] private TMP_Text airborneTempScoreText;
        [SerializeField] private TMP_Text starTempScoreText;
        [SerializeField] private float fadeDelay;
        [SerializeField] private float fadeDuration;

        private GameConfiguration _gameConfiguration;
        private InGameScreenHelper _inGameScreenHelper;
        private Tween _airborneTempScoreTween;
        private Tween _starTempScoreTween;

        public override ScreenEnum Type => ScreenEnum.InGame;

        [Inject]
        private void Initialize(ScreenSwitch screenSwitch, GameConfiguration gameConfiguration, ScoreSystem scoreSystem,
            Entities.Car car)
        {
            _inGameScreenHelper = new InGameScreenHelper(fadeDelay, fadeDuration, scoreSystem);
            _gameConfiguration = gameConfiguration;
            
            car.CarCrashed += () => screenSwitch.PickScreen(ScreenEnum.Results);
        }
        
        private void Awake()
        {
            _inGameScreenHelper.TotalScoreChange += 
                score => totalScoreText.text = $"{score}";
            _inGameScreenHelper.TempAirborneScoreChange +=
                GetBonusScoreChangeHandler(airborneTempScoreText, _airborneTempScoreTween);
            _inGameScreenHelper.TempStarScoreChange +=
                GetBonusScoreChangeHandler(starTempScoreText, _starTempScoreTween);
        }

        private void Update()
        {
            _inGameScreenHelper.Tick();
        }

        private void OnEnable()
        {   
            _inGameScreenHelper.OnEnable();
            _gameConfiguration.Paused = false;
        }

        private void OnDisable()
        {
            _gameConfiguration.Paused = true;
        }

        private Action<int> GetBonusScoreChangeHandler(TMP_Text bonusScoreText, Tween bonusScoreTween)
        {
            return scoreBonus =>
            {
                bonusScoreText.text = $"+{scoreBonus}";

                bonusScoreTween?.Kill();
                bonusScoreTween = ScoreFade(bonusScoreText);
            };
        }
        
        private Tween ScoreFade(TMP_Text text)
        {
            text.alpha = 1f;
            return text.DOFade(0, fadeDuration).SetDelay(fadeDelay);
        }
        
    }
}