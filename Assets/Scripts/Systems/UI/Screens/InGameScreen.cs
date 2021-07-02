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
        // TODO: ADD ICON AND TEXT FOR AIRBORNE AND STARS BONUSES
        [SerializeField] private TMP_Text totalScore;
        [SerializeField] private TMP_Text airborneScore;
        [SerializeField] private TMP_Text starScore;
        [SerializeField] private float fadeDelay;
        [SerializeField] private float fadeDuration;
        
        private InGameScreenHelper _inGameScreenHelper;
        
        private Tween _airborneBonusTween;
        private Tween _starBonusTween;

        [Inject]
        private void Initialize(ScoreSystem scoreSystem)
        {
            _inGameScreenHelper = new InGameScreenHelper(fadeDelay, fadeDuration, scoreSystem);
        }
        
        private void Awake()
        {
            ScreenType = ScreenEnum.InGame;
            
            _inGameScreenHelper.AiborneTempScoreChange +=
                GetBonusScoreChangeHandler(airborneScore, _airborneBonusTween);
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
                bonusScoreTween = TextFadeAway(bonusScoreText);
            };
        }
        
        private Tween TextFadeAway(TMP_Text text)
        {
            text.alpha = 1f;
            return text.DOFade(0, fadeDuration).SetDelay(fadeDelay);
        }
    }
}