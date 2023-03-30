using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class InitRuntimeData : IEcsInitSystem
    {
        private EcsWorld _world;
        private readonly GameController _gameController;

        public InitRuntimeData(GameController gameController)
        {
            _gameController = gameController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            var entity = _world.NewEntity();

            ref var runtimeData = ref _world.AddComponentToAndGet<RuntimeData>(entity);

            runtimeData.SpeedMove = _gameController.RuntimeScriptableObject.SpeedMove;
            runtimeData.SpawnInterval = _gameController.RuntimeScriptableObject.SpawnInterval;
        }
    }
}