using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class MoveAnimationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsFilter _filterPause;
        private GameController _gameController;

        public MoveAnimationSystem(GameController gameController)
        {
            _gameController = gameController;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            // _filter = _world.Filter<NeedSwitchEggAnimation>().End();
            _filter = _world.Filter<MoveData>().Inc<ViewData>().End();
            _filterPause = _world.Filter<PauseData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()|| _filterPause.IsEmpty()) return;

            foreach (var pauseEntity in _filterPause)
            foreach (var entity in _filter)
            {
                ref var moveData = ref _world.GetComponentFrom<MoveData>(entity);
                ref var viewData = ref _world.GetComponentFrom<ViewData>(entity);
                ref var pauseData = ref _world.GetComponentFrom<PauseData>(pauseEntity);

                if (pauseData.IsPause && viewData.Animator.enabled)
                {
                    viewData.Animator.enabled = false;
                    return;
                }
                if (!pauseData.IsPause && !viewData.Animator.enabled) viewData.Animator.enabled = true;

                if (moveData.EndPosition.x < moveData.Position.position.x)
                    viewData.Animator.SetInteger("Int", 0);
                else if (moveData.EndPosition.x > moveData.Position.position.x)
                    viewData.Animator.SetInteger("Int", 1);
                else
                    viewData.Animator.SetInteger("Int", 2);
                
                _world.DelComponentFrom<NeedSwitchEggAnimation>(entity);
            }
        }
    }
}