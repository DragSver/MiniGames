using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ScriptableObject;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class InitRuntimeData : IEcsInitSystem
    {
        private EcsWorld _world;
        private readonly RuntimeScriptableObject _runtimeScriptableObject;

        public InitRuntimeData(RuntimeScriptableObject runtimeScriptableObject)
        {
            _runtimeScriptableObject = runtimeScriptableObject;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            var entity = _world.NewEntity();

            ref var runtimeData = ref _world.AddComponentToAndGet<RuntimeData>(entity);

            runtimeData.SpeedMove = _runtimeScriptableObject.SpeedMove;
            runtimeData.SpawnInterval = _runtimeScriptableObject.SpawnInterval;
        }
    }
}