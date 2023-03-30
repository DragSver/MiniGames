using Leopotam.EcsLite;
using Random = UnityEngine.Random;

namespace MiniGames.WolfAndEggs.ECS
{
    public struct Entity {
        public static int Null => 0;
    }

    public static class LeoEcsLiteExtensions {
        public static bool GetTheOnlyEntity(this EcsFilter filter, out int outEntity) {
            foreach (var entity in filter) {
                outEntity = entity;
                return true;
            }

            outEntity = Entity.Null;
            return false;
        }

        public static ref T GetComponentFrom<T>(this EcsWorld world, int entityId) where T : struct {
            var pool = world.GetPool<T>();

            if (pool.Has(entityId)) {
                return ref pool.Get(entityId);
            }

            return ref pool.Add(entityId);
        }

        public static bool IsEmpty(this EcsFilter filter) {
            return filter.GetEntitiesCount() == 0;
        }

        public static void Clear(this EcsFilter filter) {
            var world = filter.GetWorld();
            foreach (var entity in filter) {
                world.DelEntity(entity);
            }
        }
        
        public static void Clear<T>(this EcsFilter filter) where T : struct {
            var world = filter.GetWorld();
            foreach (var entity in filter) {
                world.DelComponentFrom<T>(entity);
            }
        }

        public static int GetRandomEntity(this EcsFilter filter) {
            var randomIndex = Random.Range(0, filter.GetEntitiesCount());
            return filter.GetRawEntities()[randomIndex];
        }

        public static bool IsEntityAlive(this EcsWorld world, int entity) {
            return entity >= 0 && entity < world.GetAllocatedEntitiesCount() && world.GetEntityGen(entity) > 0;
        }
        
        public static ref T AddComponentToAndGet<T>(this EcsWorld world, int entityId) where T : struct {
            return ref world.GetPool<T>().Add(entityId);
        }

        public static EcsWorld AddComponentTo<T>(this EcsWorld world, int entityId) where T : struct {
            if (!world.HasComponentAt<T>(entityId)) {
                world.GetPool<T>().Add(entityId);
            }

            return world;
        }
        
        public static EcsWorld AddComponentToWithValue<T>(this EcsWorld world, int entityId, T value) where T : struct {
            world.GetPool<T>().Add(entityId) = value;
            return world;
        }

        public static bool HasComponentAt<T>(this EcsWorld world, int entityId) where T : struct {
            return world.GetPool<T>().Has(entityId);
        }
        
        public static bool HasComponentAt<T>(this EcsWorld world, int entityId, out T value) where T : struct {
            value = default;
            var pool = world.GetPool<T>();

            if (pool.Has(entityId)) {
                value = ref pool.Get(entityId);
                return true;
            }

            return false;
        }

        public static EcsWorld DelComponentFrom<T>(this EcsWorld world, int entityId) where T : struct {
            if (!world.HasComponentAt<T>(entityId)) {
                return world;
            }

            world.GetPool<T>().Del(entityId);
            return world;
        }
    }
}