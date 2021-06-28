using Systems.UI.Screens.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.Screens
{
    public class InGameScreen : MonoBehaviour, IScreen
    {
        // TODO: ADD ICON AND TEXT FOR AIRBORNE AND STARS BONUSES
        [SerializeField] private ScoreSystem scoreSystem;
        [SerializeField] private Text totalScoreText;

        private void Update()
        {
            totalScoreText.text = scoreSystem.TotalScore.ToString();
        }

        public void OnScreenEnter()
        {
            totalScoreText.text = "";
            scoreSystem.ResetScore();
        }
        
        public void OnScreenExit()
        {
        }
        
    }
}