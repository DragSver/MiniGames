using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class PauseDataInputSystem : IEcsInitSystem
    {
        private readonly GameController _gameController;
        private EcsWorld _world;

        public PauseDataInputSystem(GameController gameController)
        {
            _gameController = gameController;
        }

        public void Init(EcsSystems systems)
        {
            _gameController.SendInputPauseData+= SendInputData;
            _world = systems.GetWorld();
        }
        
        private void SendInputData()
        {
            _world.AddComponentTo<InputPause>(_world.NewEntity());
        }
    }
}