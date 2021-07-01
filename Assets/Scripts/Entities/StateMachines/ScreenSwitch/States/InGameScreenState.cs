namespace Entities.StateMachines.ScreenSwitch.States
{
    public class InGameScreenState : State
    {
        private readonly Systems.UI.ScreenSwitch _screenSwitch;

        public InGameScreenState(Systems.UI.ScreenSwitch screenSwitch)
        {
            _screenSwitch = screenSwitch;
        }

        public override void OnStateEnter()
        {
            _screenSwitch.MainMenuScreen.gameObject.SetActive(false);
            
            _screenSwitch.GameConfiguration.Paused = false;
            _screenSwitch.InGameScreen.TotalScore.text = string.Empty;
            _screenSwitch.InGameScreen.ScoreSystem.ResetScore();
        }

        public override void OnStateExit()
        {
            _screenSwitch.GameConfiguration.Paused = true;
            _screenSwitch.ResultsScreen.gameObject.SetActive(true);
        }
    }
}