using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class InitPointsSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            var entity = _world.NewEntity();

            ref var pointData = ref _world.AddComponentToAndGet<PointsData>(entity);

            pointData.Count = 0;
        }
    }
}