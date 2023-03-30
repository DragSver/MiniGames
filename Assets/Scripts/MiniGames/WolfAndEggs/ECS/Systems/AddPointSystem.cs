using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class AddPointSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private readonly GameController _gameController;

        public AddPointSystem(GameController gameController)
        {
            _gameController = gameController;
        }
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsCatch>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;

            foreach (var entity in _filter)
                _gameController.Points.Add(10);
        }
    }
}