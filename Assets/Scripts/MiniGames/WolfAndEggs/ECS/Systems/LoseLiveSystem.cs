using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class LoseLiveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterLostLive;
        private EcsFilter _filterLives;
        private readonly UIController _uiController;

        public LoseLiveSystem(UIController uiController)
        {
            _uiController = uiController;
        }

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterLostLive = _world.Filter<IsLoseLive>().End();
            _filterLives = _world.Filter<LivesData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filterLostLive.IsEmpty() || _filterLives.IsEmpty()) return;

            foreach (var entityLives in _filterLives)
            foreach (var entityLostLive in _filterLostLive)
            {
                ref var livesData = ref _world.GetComponentFrom<LivesData>(entityLives);
                livesData.Count--;
                
                _uiController.LoseLife(livesData.Count);
                
                _world.DelComponentFrom<IsLoseLive>(entityLostLive);
                _world.AddComponentTo<IsDestroy>(entityLostLive);
                if (livesData.Count == 0) 
                    _world.AddComponentTo<InputPause>(_world.NewEntity());
            }
        }
    }
}