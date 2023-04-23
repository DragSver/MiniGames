using MiniGames.WolfAndEggs.Services;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.Installers
{
    public class GameControllerInstaller : MonoInstaller
    {
        [SerializeField] private GameController _gameController;

        public override void InstallBindings()
        {
            Container.Bind<GameController>().FromInstance(_gameController).AsSingle().NonLazy();
        }
    }
}