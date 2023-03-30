using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class NextSplinePointSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<InEndPosition>().Inc<MoveData>().Inc<SplineData>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filter.IsEmpty()) return;

            foreach (var entity in _filter)
            {
                ref var splineData = ref _world.GetComponentFrom<SplineData>(entity);
                ref var moveData = ref _world.GetComponentFrom<MoveData>(entity);

                ref var splinePoints = ref splineData.SplinePoints;
                ref var numberSplinePoints = ref splineData.NumberSplinePoint;

                numberSplinePoints++;

                if (numberSplinePoints + 1 >= splinePoints.Count)
                {
                    _world.AddComponentTo<IsDestroy>(entity);
                    return;
                }
                
                if ((_world.HasComponentAt<CanCatchData>(entity) && splinePoints[numberSplinePoints].moveStatus==MoveStatus.RollingDown)
                    ||(!_world.HasComponentAt<CanCatchData>(entity) && splinePoints[numberSplinePoints].moveStatus==MoveStatus.CanCatch))
                {
                    if (splinePoints[numberSplinePoints].moveStatus == MoveStatus.CanCatch)
                    {
                        ref var catchData = ref _world.AddComponentToAndGet<CanCatchData>(entity);
                        catchData.BasketStatus = splinePoints[numberSplinePoints].BasketStatus;
                    }
                    else
                    {
                        _world.HasComponentAt<CanCatchData>(entity);
                        _world.DelComponentFrom<CanCatchData>(entity);
                    }
                }
                    
                moveData.EndPosition = splineData.SplinePoints[splineData.NumberSplinePoint + 1].Vector3;

                _world.DelComponentFrom<InEndPosition>(entity);
            }
        }
    }
}