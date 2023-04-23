using MiniGames.WolfAndEggs.Services;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.Installers
{
    public class UIControllerInstaller : MonoInstaller
    {
        [SerializeField] private UIController _uiController;

        public override void InstallBindings()
        {
            Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();
        }
    }
}