using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.ScriptableObject;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class ResetLivesDataSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterFlag;
        private readonly UIController _uiController;

        public ResetLivesDataSystem(UIController uiController)
        {
            _uiController = uiController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterFlag = _world.Filter<NewGame>().End();
            _filter = _world.Filter<LivesData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty() || _filterFlag.IsEmpty()) return;

            foreach (var entityFlag in _filterFlag)
            foreach (var entity in _filter)
            {
                ref var livesData = ref _world.GetComponentFrom<LivesData>(entity);

                livesData.Count = 3;
                _uiController.ResetLives();
            }
        }
    }
}