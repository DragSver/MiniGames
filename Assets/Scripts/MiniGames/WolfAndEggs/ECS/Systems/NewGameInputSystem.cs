using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class NewGameInputSystem : IEcsInitSystem
    {
        private readonly GameController _gameController;
        private EcsWorld _world;

        public NewGameInputSystem(GameController gameController)
        {
            _gameController = gameController;
        }

        public void Init(EcsSystems systems)
        {
            _gameController.SendInputNewGameData += SendInputData;
            _world = systems.GetWorld();
        }
        
        private void SendInputData()
        {
            _world.AddComponentTo<NewGame>(_world.NewEntity());
        }
    }
}