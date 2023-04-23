using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class BasketDataInputSystem : IEcsInitSystem
    {
        private readonly GameController _gameController;
        private EcsWorld _world;

        public BasketDataInputSystem(GameController gameController)
        {
            _gameController = gameController;
        }

        public void Init(EcsSystems systems)
        {
            _gameController.SendInputData+= SendInputData;
            _world = systems.GetWorld();
        }
        
        private void SendInputData(Vector3 position, BasketStatus status)
        {
            ref var inputData = ref _world.AddComponentToAndGet<InputBasketData>(_world.NewEntity());

            inputData.Position = position;
            inputData.Status = status;
        }
    }
}