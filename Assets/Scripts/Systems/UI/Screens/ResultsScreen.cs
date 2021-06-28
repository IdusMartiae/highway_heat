using Systems.UI.Screens.Interfaces;
using UnityEngine;

namespace Systems.UI.Screens
{
    public class ResultsScreen : MonoBehaviour, IScreen
    {
        [SerializeField] private ScoreSystem _scoreSystem;
        
        public void OnScreenEnter()
        {
        }

        public void OnScreenExit()
        {
        }
    }
}