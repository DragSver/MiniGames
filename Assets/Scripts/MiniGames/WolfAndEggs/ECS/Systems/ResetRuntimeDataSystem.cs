using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.ScriptableObject;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class ResetRuntimeDataSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterFlag;
        private readonly RuntimeScriptableObject _runtimeScriptableObject;

        public ResetRuntimeDataSystem(RuntimeScriptableObject runtimeScriptableObject)
        {
            _runtimeScriptableObject = runtimeScriptableObject;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterFlag = _world.Filter<NewGame>().End();
            _filter = _world.Filter<RuntimeData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty() || _filterFlag.IsEmpty()) return;

            foreach (var entityFlag in _filterFlag)
            foreach (var entity in _filter)
            {
                ref var runtimeData = ref _world.GetComponentFrom<RuntimeData>(entity);

                runtimeData.SpawnInterval = _runtimeScriptableObject.SpawnInterval;
                runtimeData.SpeedMove = _runtimeScriptableObject.SpeedMove;
            }
        }
    }
}