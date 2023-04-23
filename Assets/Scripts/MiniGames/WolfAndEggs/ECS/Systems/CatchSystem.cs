using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class CatchSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterEgg;
        private EcsFilter _filterBasket;
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterEgg = _world.Filter<CanCatchData>().Exc<IsCatch>().End();
            _filterBasket = _world.Filter<BasketData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filterEgg.IsEmpty() || _filterBasket.IsEmpty()) return;

            foreach (var entityBasket in _filterBasket)
            foreach (var entityEgg in _filterEgg)
            {
                ref var catchData = ref _world.GetComponentFrom<CanCatchData>(entityEgg);
                ref var basketData = ref _world.GetComponentFrom<BasketData>(entityBasket);

                if (catchData.BasketStatus == basketData.Status)
                    _world.AddComponentTo<IsCatch>(entityEgg);
            }
        }
    }
}