using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class UpdateRuntimeDataSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly GameController _gameController;
        private EcsWorld _world;
        private EcsFilter _filter;
        
        public UpdateRuntimeDataSystem(GameController gameController)
        {
            _gameController = gameController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RuntimeData>().End();
        }
        
        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;

            foreach (var entity in _filter)
            {
                ref var runtimeData = ref _world.GetComponentFrom<RuntimeData>(entity);

                var point = _gameController.Points.Point/1000;
                
                runtimeData.SpeedMove = (point + _gameController.RuntimeScriptableObject.SpeedMove) * Time.deltaTime;
                
                var spawnInterval = _gameController.RuntimeScriptableObject.SpawnInterval;
                runtimeData.SpawnInterval = spawnInterval-point > 0.1f ? spawnInterval-point : 0.1f;
            }
        }
    }
}