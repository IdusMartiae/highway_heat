using System.Collections.Generic;
using Systems.UI.Screens;
using Entities.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems.UI
{
    public class ScreenSwitch : MonoBehaviour
    {
        [SerializeField] private List<BaseScreen> screens;
        
        private Dictionary<ScreenEnum, BaseScreen> _screens;

        public ScreenEnum ActiveScreen { get; private set; } = ScreenEnum.MainMenu;
        
        private void Awake()
        {
            InitializeScreenDictionary();
        }
        
        public void PickScreen(ScreenEnum screenType)
        {
            foreach (var screen in _screens.Values)
            {
                screen.gameObject.SetActive(false);
            }
            
            _screens[screenType].gameObject.SetActive(true);
            ActiveScreen = screenType;
            
            if (screenType == ScreenEnum.MainMenu) ReloadScene();
        }
        
        private void InitializeScreenDictionary()
        {
            _screens = new Dictionary<ScreenEnum, BaseScreen>();
            foreach (var screen in screens)
            {
                _screens.Add(screen.Type, screen);
            }
        }

        private void ReloadScene()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}