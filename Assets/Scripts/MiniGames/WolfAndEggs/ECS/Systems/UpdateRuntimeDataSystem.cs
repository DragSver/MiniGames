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
        private EcsFilter _filterPoints;
        
        public UpdateRuntimeDataSystem(GameController gameController)
        {
            _gameController = gameController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<RuntimeData>().End();
            _filterPoints = _world.Filter<PointsData>().End();
        }
        
        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty() || _filterPoints.IsEmpty()) return;

            foreach (var entityPoints in _filterPoints)
            foreach (var entity in _filter)
            {
                ref var pointsData = ref _world.GetComponentFrom<PointsData>(entityPoints);
                ref var runtimeData = ref _world.GetComponentFrom<RuntimeData>(entity);

                var point = pointsData.Count/100;
                
                runtimeData.SpeedMove = (point + _gameController.RuntimeScriptableObject.SpeedMove) * Time.deltaTime;
                
                var spawnInterval = _gameController.RuntimeScriptableObject.SpawnInterval;
                runtimeData.SpawnInterval = spawnInterval-point > 0.1f ? spawnInterval-point : 0.1f;
            }
        }
    }
}