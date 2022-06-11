using Leopotam.Ecs;
using UnityEngine;
namespace AffenCode
{
    public class SyncFromUnityTransformSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilter<EcsTransform, TransformRef>.Exclude<IgnoreTransformSync> _filterTransforms;
        private EcsFilter<EcsTransform, RectTransformRef>.Exclude<IgnoreTransformSync> _filterRectTransforms;
        private EcsFilter<EcsTransform, RigidbodyRef, IgnoreTransformSync>.Exclude<IgnoreRigidbodySync> _filterRigidbody;
        private EcsFilter<EcsTransform, Rigidbody2DRef, IgnoreTransformSync>.Exclude<IgnoreRigidbodySync> _filterRigidbody2D;

        public void PreInit()
        {
            SyncTransforms();
        }

        public void Run()
        {
            SyncTransforms();
        }

        private void SyncTransforms()
        {
            foreach (var entityIndex in _filterTransforms)
            {
                ref var transform = ref _filterTransforms.Get1(entityIndex);
                ref var unityTransform = ref _filterTransforms.Get2(entityIndex);
                transform.Position = unityTransform.Value.position;
                transform.Rotation = unityTransform.Value.rotation;
                transform.Scale = unityTransform.Value.localScale;
            }
            
            foreach (var entityIndex in _filterRectTransforms)
            {
                ref var transform = ref _filterRectTransforms.Get1(entityIndex);
                ref var rectTransformRef = ref _filterRectTransforms.Get2(entityIndex);
                transform.Position = rectTransformRef.Value.anchoredPosition;
                transform.Rotation = rectTransformRef.Value.rotation;
                transform.Scale = rectTransformRef.Value.localScale;
            }
            
            foreach (var entityIndex in _filterRigidbody)
            {
                ref var ecsTransform = ref _filterRigidbody.Get1(entityIndex);
                ref var rigidbodyRef = ref _filterRigidbody.Get2(entityIndex);
                ecsTransform.Position = rigidbodyRef.Value.position;
                ecsTransform.Rotation = rigidbodyRef.Value.rotation;
            }
            
            foreach (var entityIndex in _filterRigidbody2D)
            {
                ref var ecsTransform = ref _filterRigidbody2D.Get1(entityIndex);
                ref var rigidbodyRef = ref _filterRigidbody2D.Get2(entityIndex);
                ecsTransform.Position = new(rigidbodyRef.Value.position.x, rigidbodyRef.Value.position.y, ecsTransform.Position.z);
                ecsTransform.Rotation = Quaternion.Euler(0, 0, rigidbodyRef.Value.rotation);
            }
        }
    }
}
