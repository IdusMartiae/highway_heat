using Systems.UI.Screens.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.Screens
{
    public class InGameScreen : MonoBehaviour, IScreen
    {
        // TODO: ADD ICON AND TEXT FOR AIRBORNE AND STARS BONUSES
        [SerializeField] private ScoreSystem scoreSystem;
        // TODO: use TextMeshPro
        [SerializeField] private Text totalScoreText;

        private void Update()
        {
            // TODO: update ui on events
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