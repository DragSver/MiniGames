using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Systems;
using MiniGames.WolfAndEggs.ScriptableObject;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;
using Zenject;

namespace MiniGames.WolfAndEggs.ECS {
    sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        [SerializeField] private RuntimeScriptableObject _runtimeScriptableObject;
        [Inject] private GameController _gameController;
        [Inject] private UIController _uiController;
        
        void Start ()
        {
            // register your shared data here, for example:
            // var shared = new Shared ();
            // systems = new EcsSystems (new EcsWorld (), shared);
            _world = new EcsWorld();
            _systems = new EcsSystems (_world);
            // _systems = new EcsSystems (new EcsWorld ());
            _systems
                // register your systems here, for example:
                // .Add (new TestSystem1 ())
                // .Add (new TestSystem2 ())
                .Add(new InitLivesSystem())
                .Add(new InitPointsSystem())
                
                .Add(new InitRuntimeData(_runtimeScriptableObject))
                .Add(new UpdateRuntimeDataSystem(_runtimeScriptableObject))
                
                .Add(new InitPauseSystem())
                .Add(new SwitchPauseSystem(_uiController))
                .Add(new PauseDataInputSystem(_gameController))
                
                .Add(new InitBasketSystem(_gameController, _runtimeScriptableObject))
                .Add(new InitEggsSystem(_gameController))
                
                .Add(new BasketDataInputSystem(_gameController))
                .Add(new MoveBasketSystem(_runtimeScriptableObject))
                .Add(new SwitchBasketStatusSystem())
                
                .Add(new MoveSystem())
                // .Add(new MoveViewSystem())
                .Add(new NextSplinePointSystem())
                .Add(new MoveAnimationSystem(_gameController))
                
                .Add(new CatchSystem())
                
                .Add(new AddPointSystem(_uiController))
                .Add(new LoseLiveSystem(_uiController))
                .Add(new DestroySystem())

                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                // add here cleanup for custom worlds, for example:
                // _systems.GetWorld ("events").Destroy ();
                _systems.GetWorld ().Destroy ();
                _systems = null;
            }
        }
    }
}