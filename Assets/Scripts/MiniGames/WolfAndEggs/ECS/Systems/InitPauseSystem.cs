using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class InitPauseSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            var entity = _world.NewEntity();

            ref var pauseData = ref _world.AddComponentToAndGet<PauseData>(entity);

            pauseData.IsPause = false;
        }
    }
}