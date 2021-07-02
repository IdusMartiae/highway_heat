using System.Collections.Generic;
using Systems.UI.Screens;
using Entities.Enums;
using UnityEngine;

namespace Systems.UI
{
    public class ScreenSwitch : MonoBehaviour
    {
        [SerializeField] private Dictionary<ScreenEnum, BaseScreen> screens;
    }
}

        /*[SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private Entities.Car car;
        [SerializeField] private MainMenuScreen mainMenuScreen;
        [SerializeField] private InGameScreen inGameScreen;
        [SerializeField] private ResultsScreen resultsScreen;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button playButton;

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {

            // playButton.onClick.AddListener(() => { _screenStateMachine.TransitionToState(inGameScreenState); });
            // car.CarCrashed += () => _screenStateMachine.TransitionToState(resultsScreenState);
            // retryButton.onClick.AddListener(ReloadScene);
            
        }

        private void ReloadScene()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }*/

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