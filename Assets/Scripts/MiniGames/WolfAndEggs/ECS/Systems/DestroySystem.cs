using Leopotam.EcsLite;
using MiniGames.WolfAndEggs.ECS.Components;
using MiniGames.WolfAndEggs.ECS.Components.Flags;
using UnityEngine;

namespace MiniGames.WolfAndEggs.ECS.Systems
{
    public class DestroySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterDestroy;
        
        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterDestroy = _world.Filter<IsDestroy>().End();
        }

        public void Run(EcsSystems systems)
        {
            if (_filterDestroy.IsEmpty()) return;

            foreach (var entity in _filterDestroy)
                Delete(entity);
        }

        private void Delete(int entity)
        {
            if (_world.HasComponentAt<ViewData>(entity))
            {
                ref var viewData = ref _world.GetComponentFrom<ViewData>(entity);
                Object.Destroy(viewData.GameObject);
            }
            _world.DelEntity(entity);
        }
    }
}