using Leopotam.Ecs;
using UnityEngine;
namespace AffenCode
{
    public static class TransformEx
    {
        public static EcsEntity AddGameObject(this EcsEntity entity, GameObject gameObject)
        {
            return entity
                .Replace(new GameObjectRef()
                {
                    Value = gameObject
                });
        }
        
        public static EcsEntity AddTransform(this EcsEntity entity, Transform transform)
        {
            return entity
                .Replace(new EcsTransform
                {
                    Position = transform.position,
                    Rotation = transform.rotation
                })
                .Replace(new TransformRef
                {
                    Value = transform
                });
        }

        public static EcsEntity AddRigidbody(this EcsEntity entity, Rigidbody rigidbody)
        {
            return entity
                .Replace(new RigidbodyRef
                {
                    Value = rigidbody
                })
                .Replace(new IgnoreTransformSync());
        }
    }
}