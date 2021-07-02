using System.Collections.Generic;
using Systems.UI.Screens;
using Configurations;
using Entities.StateMachines;
using Entities.StateMachines.ScreenSwitch.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Systems.UI
{
    public class ScreenSwitch : MonoBehaviour
    {
        // TODO: ADD SCREEN ENUM AND "NEXT SCREEN" FIELDS TO SCREENS, IF TRANSITION OCCUR RETURN NEXT SCREEN VALUE FROM CURRENT ONE INTO SWITCH CONSTRUCTION
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private Entities.Car car;
        [SerializeField] private MainMenuScreen mainMenuScreen;
        [SerializeField] private InGameScreen inGameScreen;
        [SerializeField] private ResultsScreen resultsScreen;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button playButton;

        private StateMachine _screenStateMachine;

        public GameConfiguration GameConfiguration => gameConfiguration;
        public MainMenuScreen MainMenuScreen => mainMenuScreen;
        public InGameScreen InGameScreen => inGameScreen;
        public ResultsScreen ResultsScreen => resultsScreen;

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            var mainMenuScreenState = new MainMenuScreenState(this);
            var inGameScreenState = new InGameScreenState(this);
            var resultsScreenState = new ResultsScreenState(this);

            playButton.onClick.AddListener(() => { _screenStateMachine.TransitionToState(inGameScreenState); });
            car.CarCrashed += () => _screenStateMachine.TransitionToState(resultsScreenState);
            retryButton.onClick.AddListener(ReloadScene);

            _screenStateMachine = new StateMachine(mainMenuScreenState);
            mainMenuScreenState.OnStateEnter();
        }

        private void ReloadScene()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }
}

// TODO: option for ScreenSwitcher
// [SerializeField] List<Screen> screens;
//
// private Dictionary<ScreenType, Screen> _screens;
//
// public void PickScreen(ScreenType screenType)
// {
//     foreach (var screen in _screens.Values)
//     {
//         // Can be a simple screen.gameObject.SetActive(false)
//         screen.Deactivate();
//     }
//
//     // Can be a simple screen.gameObject.SetActive(true)
//     _screens[screenType].Activate();
// }
//
// private void InitializeScreenDictionary()
// {
//     _screens = new Dictionary<ScreenType, Screen>();
//     foreach (var screen in screens)
//     {
//         _screens.Add(screen.Type, screen);
//     }
// }