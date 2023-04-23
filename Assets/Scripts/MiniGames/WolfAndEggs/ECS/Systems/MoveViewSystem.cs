using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class MoveViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<MoveData>().Inc<ViewData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;

            foreach (var entity in _filter)
            {
                ref var viewData = ref _world.GetComponentFrom<ViewData>(entity);
                ref var moveData = ref _world.GetComponentFrom<MoveData>(entity);

                viewData.GameObject.transform.position = moveData.Position.position;
            }
        }
    }
}