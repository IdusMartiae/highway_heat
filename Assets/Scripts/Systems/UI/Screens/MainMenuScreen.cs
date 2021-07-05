using Entities.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Systems.UI.Screens
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private Button playButton;
        [SerializeField] private TMP_Text money;
        [SerializeField] private TMP_Text highScore;

        private PlayerDataService _playerDataService;
        
        public override ScreenEnum Type => ScreenEnum.MainMenu;

        [Inject]
        private void Initialize(ScreenSwitch screenSwitch, PlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
            playButton.onClick.AddListener(() => screenSwitch.PickScreen(ScreenEnum.InGame));
        }

        private void OnEnable()
        {
            money.text = _playerDataService.GetPlayerMoney().ToString();
            highScore.text = _playerDataService.GetPlayerHighScore().ToString();
        }
    }
}
