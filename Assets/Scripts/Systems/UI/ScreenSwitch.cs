using Systems.UI.Screens;
using UnityEngine;

namespace Systems.UI
{
    public class ScreenSwitch : MonoBehaviour
    {
        // TODO: ADD SCREEN ENUM AND "NEXT SCREEN" FIELDS TO SCREENS, IF TRANSITION OCCUR RETURN NEXT SCREEN VALUE FROM CURRENT ONE INTO SWITCH CONSTRUCTION
        [SerializeField] private Entities.Car car;
        [SerializeField] private MainMenuScreen mainMenuScreen;
        [SerializeField] private InGameScreen inGameScreen;
        [SerializeField] private ResultsScreen resultsScreen;
        
        private void Awake()
        {
            // InitializeUI();
            // car.CarCrashed += ;
        }

        private void Update()
        {
        }

        // private void SetNextScreenToResults()
        // {
        //     _currentScreen.OnScreenExit();
        //     _currentScreen = resultsScreen;
        //     _currentScreen.OnScreenEnter();
        // }
        //
        // private void InitializeUI()
        // {
        //     // inGameScreen.gameObject.SetActive(false);
        //     // resultsScreen.gameObject.SetActive(false);
        //     // mainMenuScreen.gameObject.SetActive(true);
        //
        //     _currentScreen = inGameScreen;
        //     _currentScreen.OnScreenEnter();
        // }
    }
}