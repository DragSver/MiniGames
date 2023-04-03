using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class InitLivesSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            var livesEntity = _world.NewEntity();

            ref var livesData = ref _world.AddComponentToAndGet<LivesData>(livesEntity);

            livesData.Count = 3;
        }
    }
}