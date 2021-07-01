using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Systems.UI.Screens
{
    public class InGameScreen : MonoBehaviour
    {
        // TODO: ADD ICON AND TEXT FOR AIRBORNE AND STARS BONUSES
        [SerializeField] private ScoreSystem scoreSystem;
        [SerializeField] private TMP_Text totalScore;
        [SerializeField] private TMP_Text airborneScore;
        [SerializeField] private TMP_Text starScore;
        [SerializeField] private float fadeOutDelay;
        [SerializeField] private float fadeOutSpeed;

        private int _airborneBonus;
        private int _starBonus;
        private Tween _airborneBonusTween;
        private Tween _starBonusTween;
        
        private void Awake()
        {
            scoreSystem.TotalScoreChange += HandleTotalScoreChange;
            scoreSystem.AirborneScoreChange += HandleAirborneBonusChange;
            scoreSystem.StarScoreChange += HandleStarBonusChange;
        }

        public void OnScreenEnter()
        {
            totalScore.text = "";
            scoreSystem.ResetScore();
        }

        public void OnScreenExit()
        {
        }

        private void HandleTotalScoreChange(int newScore)
        {
            totalScore.text = $"{newScore}";
        }

        private void HandleAirborneBonusChange(int scoreBonus)
        {
            _airborneBonus += scoreBonus;
            airborneScore.text = $"+{_airborneBonus}";
            
            _airborneBonusTween?.Kill();
            _airborneBonusTween = TextFadeAway(airborneScore).OnComplete(() => _airborneBonus = 0);
        }

        private void HandleStarBonusChange(int scoreBonus)
        {
            _starBonus += scoreBonus;
            starScore.text = $"+{_starBonus}";

            _starBonusTween?.Kill();
            _starBonusTween = TextFadeAway(starScore).OnComplete(() => _starBonus = 0);
        }

        private Tween TextFadeAway(TMP_Text text)
        {
            text.alpha = 1f;
            return text.DOFade(0, 10 / fadeOutSpeed).SetDelay(fadeOutDelay);
        }
    }
}