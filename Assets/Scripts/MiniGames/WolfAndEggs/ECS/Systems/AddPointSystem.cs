using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class AddPointSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterCatch;
        private EcsFilter _filterPoints;
        private readonly UIController _uiController;

        public AddPointSystem(UIController uiController)
        {
            _uiController = uiController;
        }

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterCatch = _world.Filter<IsCatch>().End();
            _filterPoints = _world.Filter<PointsData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filterCatch.IsEmpty() || _filterPoints.IsEmpty()) return;

            foreach (var entityPoints in _filterPoints)
            foreach (var entityCatch in _filterCatch)
            {
                ref var pointsData = ref _world.GetComponentFrom<PointsData>(entityPoints);
                pointsData.Count += 10;
                
                _uiController.PointsUpdate(pointsData.Count);
                
                _world.DelComponentFrom<IsCatch>(entityCatch);
                _world.AddComponentTo<IsDestroy>(entityCatch);
            }
        }
    }
}