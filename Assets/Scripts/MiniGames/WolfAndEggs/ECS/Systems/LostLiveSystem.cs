using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class LostLiveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private GameController _gameController;

        public LostLiveSystem(GameController gameController)
        {
            _gameController = gameController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsDestroy>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;

            foreach (var entity in _filter)
                _gameController.Lives.LostLive();
        }
    }
}