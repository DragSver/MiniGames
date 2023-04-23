using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class DestroyAllEggsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterFlag;
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterFlag = _world.Filter<NewGame>().End();
            _filter = _world.Filter<MoveData>().Inc<ViewData>().Inc<SplineData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty() || _filterFlag.IsEmpty()) return;

            foreach (var entityFlag in _filterFlag)
            foreach (var entity in _filter)
            {
                _world.AddComponentTo<IsDestroy>(entity);
            }
        }
    }
}