using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.ScriptableObject;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class InitBasketSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        private readonly GameController _gameController;

        public InitBasketSystem(GameController gameController)
        {
            _gameController = gameController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            var basketEntity = _world.NewEntity();

            ref var basketData = ref _world.AddComponentToAndGet<BasketData>(basketEntity);
            
            basketData.GameObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Basket"),
                _gameController.StartBasketPosition.transform.position, Quaternion.identity);
            basketData.BasketStatus = _gameController.RuntimeScriptableObject.StartBasketStatus;
        }
    }
}