﻿using DG.Tweening;
using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.ScriptableObject;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class MoveBasketSystem : IEcsInitSystem,
                                    IEcsRunSystem
    {
        
        private EcsWorld _world;
        private EcsFilter _filterInput;
        private EcsFilter _filterBasket;
        private readonly GameController _gameController;

        public MoveBasketSystem(GameController gameController)
        {
            _gameController = gameController;
        }

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filterInput = _world.Filter<InputBasketData>().End();
            _filterBasket = _world.Filter<BasketData>().End();
        }
        
        public void Run(EcsSystems systems)
        {
            if (_filterInput.IsEmpty() || _filterBasket.IsEmpty()) return;

            foreach (var entityInput in _filterInput)
            foreach (var entityBasket in _filterBasket)
            {
                var inputData = _world.GetComponentFrom<InputBasketData>(entityInput);
                var inputBasket = _world.GetComponentFrom<BasketData>(entityBasket);

                inputBasket.GameObject.transform.DOMove(inputData.Position, _gameController.RuntimeScriptableObject.SpeedBasket);
            }
        }
    }
}