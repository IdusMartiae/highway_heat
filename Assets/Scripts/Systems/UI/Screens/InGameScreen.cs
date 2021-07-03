using System;
using Systems.UI.Screens.Helpers;
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
        
        private InGameScreenHelper _inGameScreenHelper;
        
        private Tween _airborneTempScoreTween;
        private Tween _starTempScoreTween;

        [Inject]
        private void Initialize(ScoreSystem scoreSystem)
        {
            _inGameScreenHelper = new InGameScreenHelper(fadeDelay, fadeDuration, scoreSystem);
        }
        
        private void Start()
        {
            ScreenType = ScreenEnum.InGame;

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