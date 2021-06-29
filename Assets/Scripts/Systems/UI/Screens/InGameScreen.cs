using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Systems.UI.Screens
{
    public class InGameScreen : MonoBehaviour
    {
        // TODO: ADD ICON AND TEXT FOR AIRBORNE AND STARS BONUSES
        [SerializeField] private ScoreSystem scoreSystem;
        [SerializeField] private TextMeshProUGUI totalScore;
        [SerializeField] private TextMeshProUGUI airborneScore;
        [SerializeField] private TextMeshProUGUI starScore;
        [SerializeField] private float fadeOutDelay;
        [SerializeField] private float fadeOutSpeed;
        
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
            airborneScore.text = $"+{scoreBonus}";
            TextFadeAway(airborneScore);
        }
        
        private void HandleStarBonusChange(int scoreBonus)
        {
            starScore.text = $"+{scoreBonus}";
            TextFadeAway(starScore);
        }

        private void TextFadeAway(TMP_Text textMeshProUGUI)
        {
            textMeshProUGUI.alpha = 1f;
            textMeshProUGUI.DOFade(0, 10 / fadeOutSpeed).SetDelay(fadeOutDelay);
        }
    }
}