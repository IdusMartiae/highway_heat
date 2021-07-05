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
        [SerializeField] private Car car;

        public override void InstallBindings()
        {
            Container.BindInstance(screenSwitch);
            Container.BindInstance(car);
        }
    }
}