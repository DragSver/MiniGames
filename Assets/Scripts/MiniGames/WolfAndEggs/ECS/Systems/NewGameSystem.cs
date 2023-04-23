using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class NewGameSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterFlag;
        private readonly UIController _uiController;

        public NewGameSystem(UIController uiController)
        {
            _uiController = uiController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterFlag = _world.Filter<NewGame>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filterFlag.IsEmpty()) return;

            foreach (var entityFlag in _filterFlag)
            {
                _world.DelEntity(entityFlag);
                _world.AddComponentTo<InputPause>(_world.NewEntity());
                _uiController.EndGame();
            }
        }
    }
}