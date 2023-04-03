using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class MoveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterRuntime;
        private EcsFilter _filterPause;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<MoveData>().End();
            _filterRuntime = _world.Filter<RuntimeData>().End();
            _filterPause = _world.Filter<PauseData>().End();
        }
        
        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty() || _filterPause.IsEmpty()) return;

            foreach (var pauseEntity in _filterPause)
            {
                ref var pauseData = ref _world.GetComponentFrom<PauseData>(pauseEntity);
                if (pauseData.IsPause) return;
            }

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