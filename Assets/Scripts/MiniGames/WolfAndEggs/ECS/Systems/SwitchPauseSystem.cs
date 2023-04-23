using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class SwitchPauseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterPause;
        private readonly UIController _uiController;

        public SwitchPauseSystem(UIController uiController)
        {
            _uiController = uiController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PauseData>().End();
            _filterPause = _world.Filter<InputPause>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty() || _filterPause.IsEmpty()) return;

            foreach (var entityPause in _filterPause)
            foreach (var entity in _filter)
            {
                ref var pauseData = ref _world.GetComponentFrom<PauseData>(entity);
                pauseData.IsPause = !pauseData.IsPause;
                
                _uiController.SwitchPauseButton();
                _world.DelEntity(entityPause);
            }
        }
    }
}