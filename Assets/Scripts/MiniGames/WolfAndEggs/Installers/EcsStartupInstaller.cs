using MiniGames.WolfAndEggs.ECS;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.Installers {
    sealed class EcsStartupInstaller : MonoInstaller
    {
        [SerializeField] private EcsStartup _ecsStartup;

        public override void InstallBindings()
        {
            Container.Bind<EcsStartup>().FromInstance(_ecsStartup).AsSingle().NonLazy();
        }
    }
}