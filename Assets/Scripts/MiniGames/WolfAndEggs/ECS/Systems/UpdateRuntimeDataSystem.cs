﻿using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ScriptableObject;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class UpdateRuntimeDataSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly RuntimeScriptableObject _runtimeScriptableObject;
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterPoints;
        
        public UpdateRuntimeDataSystem(RuntimeScriptableObject runtimeScriptableObject)
        {
            _runtimeScriptableObject = runtimeScriptableObject;
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

                var point = pointsData.Count/100f;
                
                runtimeData.SpeedMove = (point + _runtimeScriptableObject.SpeedMove) * Time.deltaTime<=3.5?
                    (point + _runtimeScriptableObject.SpeedMove) * Time.deltaTime:3.5f;
                
                var spawnInterval = _runtimeScriptableObject.SpawnInterval;
                runtimeData.SpawnInterval = spawnInterval-point > 1f ? spawnInterval-point : 1f;
            }
        }
    }
}