using Leopotam.Ecs;
namespace AffenCode
{
    public static class EntityEx
    {
        public static bool TryGet<T>(this EcsEntity entity, ref T component) where T : struct
        {
            if (!entity.Has<T>())
            {
                return false;
            }
            
            component = entity.Get<T>();
            return true;
        }
    }
}
