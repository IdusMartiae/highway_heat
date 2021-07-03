using Entities.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Systems.UI.Screens
{
    public class MainMenuScreen : BaseScreen
    {
        [SerializeField] private Button playButton;

        public override ScreenEnum Type => ScreenEnum.MainMenu;

        [Inject]
        private void Initialize(ScreenSwitch screenSwitch)
        {
            playButton.onClick.AddListener(() => screenSwitch.PickScreen(ScreenEnum.InGame));
        }

    }
}
