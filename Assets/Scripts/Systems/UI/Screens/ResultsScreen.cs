using TMPro;
using UnityEngine;

namespace Systems.UI.Screens
{
    public class ResultsScreen : MonoBehaviour
    {
        [SerializeField] private ScoreSystem scoreSystem;
        [SerializeField] private TMP_Text totalScore;
        [SerializeField] private TMP_Text distanceScore;
        [SerializeField] private TMP_Text starsScore;
        [SerializeField] private TMP_Text airTimeScore;

        private void OnEnable()
        {
            SetAllFields();
        }

        private void SetAllFields()
        {
            totalScore.text = scoreSystem.TotalScore.ToString();
            distanceScore.text = scoreSystem.DistanceBonus.ToString();
            starsScore.text = scoreSystem.StarBonus.ToString();
            airTimeScore.text = scoreSystem.AirborneBonus.ToString();
        }
        
    }
}