using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.ScriptableObject;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class ResetPointsDataSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterFlag;
        private readonly UIController _uiController;

        public ResetPointsDataSystem(UIController uiController)
        {
            _uiController = uiController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterFlag = _world.Filter<NewGame>().End();
            _filter = _world.Filter<PointsData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty() || _filterFlag.IsEmpty()) return;

            foreach (var entityFlag in _filterFlag)
            foreach (var entity in _filter)
            {
                ref var pointsData = ref _world.GetComponentFrom<PointsData>(entity);

                pointsData.Count = 0;
                _uiController.PointsUpdate(0);
            }
        }
    }
}