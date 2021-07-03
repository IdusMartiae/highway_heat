using Systems;
using Systems.UI;
using Configurations;
using Entities;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private ScreenSwitch screenSwitch;
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private ScoreSystem scoreSystem;
        [SerializeField] private Car car;

        public override void InstallBindings()
        {
            Container.BindInstance(screenSwitch);
            Container.BindInstance(gameConfiguration);
            Container.BindInstance(scoreSystem);
            Container.BindInstance(car);
        }
    }
}