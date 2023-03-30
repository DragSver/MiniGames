using Leopotam.EcsLite;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class DefaultSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            // _filter = _world.Filter<>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;

            foreach (var entity in _filter)
            {
                
            }
        }
    }
}