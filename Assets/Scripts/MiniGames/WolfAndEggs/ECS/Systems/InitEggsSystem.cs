using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using MiniGames.WolfAndEggs.Services;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class InitEggsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly System.Random _random;
        private readonly GameObject _eggPrefab;
        private readonly GameController _gameController;
        
        private EcsWorld _world;
        private EcsFilter _filterRuntime;
        private EcsFilter _filterPause;
        
        private float _nextSpawnTime;

        public InitEggsSystem(GameController gameController)
        {
            _gameController = gameController;

            _random = new System.Random();
            _eggPrefab = Resources.Load<GameObject>("Prefabs/Egg");
            _nextSpawnTime = Time.time;
        }
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterRuntime = _world.Filter<RuntimeData>().End();
            _filterPause = _world.Filter<PauseData>().End();
        }
        
        public void Run(EcsSystems systems)
        {
            if (_filterPause.IsEmpty()) return;

            foreach (var pauseEntity in _filterPause)
            {
                ref var pauseData = ref _world.GetComponentFrom<PauseData>(pauseEntity);
                if (pauseData.IsPause)
                {
                    Time.timeScale = 0;
                    return;
                }
                Time.timeScale = 1;
            }

            if (!(Time.time > _nextSpawnTime)) return;
            
            foreach (var entity in _filterRuntime)
            {
                ref var runtimeData = ref _world.GetComponentFrom<RuntimeData>(entity);
                
                var numberSpawnPlace = _random.Next(0, _gameController.ListSplinePoints.Count);
                var spawnPlace = _gameController.ListSplinePoints[numberSpawnPlace];

                var egg = _world.NewEntity();
            
                ref var viewData = ref _world.AddComponentToAndGet<ViewData>(egg);
            
                viewData.GameObject = Object.Instantiate(_eggPrefab, spawnPlace.SplinePoints[0].Vector3, Quaternion.identity);
                viewData.Animator = viewData.GameObject.GetComponent<Animator>();

                ref var splineData = ref _world.AddComponentToAndGet<SplineData>(egg);
            
                splineData.SplinePoints = spawnPlace.SplinePoints;
                splineData.NumberSplinePoint = -1;

                ref var moveData = ref _world.AddComponentToAndGet<MoveData>(egg);

                moveData.Position = viewData.GameObject.transform;
                moveData.EndPosition = splineData.SplinePoints[0].Vector3;

                _nextSpawnTime += runtimeData.SpawnInterval;

                _world.AddComponentTo<NeedSwitchEggAnimation>(egg);
            }
        }
    }
}