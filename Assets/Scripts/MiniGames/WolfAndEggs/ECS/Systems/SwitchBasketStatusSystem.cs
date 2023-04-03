using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class SwitchBasketStatusSystem : IEcsInitSystem,
                                            IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _filterInput;
        private EcsFilter _filterBasket;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();

            _filterInput = _world.Filter<InputBasketData>().End();
            _filterBasket = _world.Filter<BasketData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filterInput.IsEmpty() || _filterBasket.IsEmpty()) return;

            foreach (var entityInput in _filterInput)
            foreach (var entityBasket in _filterBasket)
            {
                ref var inputData = ref _world.GetComponentFrom<InputBasketData>(entityInput);
                ref var inputBasket = ref _world.GetComponentFrom<BasketData>(entityBasket);

                inputBasket.Status = inputData.Status;
                
                _world.DelEntity(entityInput);
            }
        }
    }
}