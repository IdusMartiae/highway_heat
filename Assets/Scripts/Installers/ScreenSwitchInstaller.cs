using Systems;
using Systems.UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ScreenSwitchInstaller : MonoInstaller
    {
        [SerializeField] private ScreenSwitch screenSwitch;
        [SerializeField] private ScoreSystem scoreSystem;

        public override void InstallBindings()
        {
            Container.BindInstance(screenSwitch);
            Container.BindInstance(scoreSystem);
        }
    }
}