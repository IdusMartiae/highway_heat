using Configurations;
using Entities.Factories.Interfaces;
using Entities.Spawners;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameEntitySpawnerInstaller : MonoInstaller
    {
        [SerializeField] private GameEntityConfiguration gameEntityConfiguration;
        [SerializeField] private GameEntitySpawner gameEntitySpawner;

        public override void InstallBindings()
        {
            Container.BindInstance(gameEntityConfiguration);
            Container.Bind<IGameEntityFactory>().FromInstance(gameEntitySpawner.GameEntityFactory);
        }
    }
}