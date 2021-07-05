using Configurations;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class RoadRendererInstaller : MonoInstaller
    {
        [SerializeField] private InputConfiguration inputConfiguration;
        [SerializeField] private InputHandler inputHandler;
    
        public override void InstallBindings()
        {
            Container.BindInstance(inputConfiguration);
            Container.BindInstance(inputHandler);
        }
    }
}