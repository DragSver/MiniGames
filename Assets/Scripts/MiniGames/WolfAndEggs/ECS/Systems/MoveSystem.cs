using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class MoveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterRuntime;
        private readonly GameController _gameController;

        public MoveSystem(GameController gameController)
        {
            _gameController = gameController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<MoveData>().End();
            _filterRuntime = _world.Filter<RuntimeData>().End();
        }
        
        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;
            
            if (_gameController.IsPause) return;

            foreach (var entity in _filter)
            foreach (var entityRuntime in _filterRuntime)
            {
                ref var moveData = ref _world.GetComponentFrom<MoveData>(entity);
                ref var runtimeData = ref _world.GetComponentFrom<RuntimeData>(entityRuntime);

                moveData.Position.position = Vector3.MoveTowards(moveData.Position.position, moveData.EndPosition, runtimeData.SpeedMove);

                if (moveData.Position.position == moveData.EndPosition) _world.AddComponentTo<InEndPosition>(entity);
            }
        }
    }
}