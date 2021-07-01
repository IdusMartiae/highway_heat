namespace Entities.StateMachines.ScreenSwitch.States
{
    public class MainMenuScreenState : State
    {
        private readonly Systems.UI.ScreenSwitch _screenSwitch;
        public MainMenuScreenState(Systems.UI.ScreenSwitch screenSwitch)
        {
            _screenSwitch = screenSwitch;
        }

        public override void OnStateEnter()
        {
            _screenSwitch.ResultsScreen.gameObject.SetActive(false);
        }

        public override void OnStateExit()
        {
            _screenSwitch.InGameScreen.gameObject.SetActive(true);
        }
    }
}