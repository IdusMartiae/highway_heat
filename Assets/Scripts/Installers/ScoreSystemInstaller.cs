using Systems;
using Configurations;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ScoreSystemInstaller : MonoInstaller
    {
        [SerializeField] private ScoreSystem scoreSystem;
        [SerializeField] private GameConfiguration gameConfiguration;
        
        public override void InstallBindings()
        {
            var playerDataService = new PlayerDataService(scoreSystem, gameConfiguration);
            Container.BindInstance(scoreSystem);
            Container.BindInstance(gameConfiguration);
            Container.BindInstance(playerDataService);
        }
    }
}