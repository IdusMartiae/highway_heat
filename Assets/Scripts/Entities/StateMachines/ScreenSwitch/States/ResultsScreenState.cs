namespace Entities.StateMachines.ScreenSwitch.States
{
    public class ResultsScreenState : State
    {
        private readonly Systems.UI.ScreenSwitch _screenSwitch;

        public ResultsScreenState(Systems.UI.ScreenSwitch screenSwitch)
        {
            _screenSwitch = screenSwitch;
        }

        public override void OnStateEnter()
        {
            _screenSwitch.InGameScreen.gameObject.SetActive(false);
        }

        public override void OnStateExit()
        {
            _screenSwitch.MainMenuScreen.gameObject.SetActive(true);
        }
    }
}