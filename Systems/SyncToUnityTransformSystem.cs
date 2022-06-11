using Leopotam.Ecs;
namespace AffenCode
{
    public class SyncToUnityTransformSystem : IEcsPreInitSystem, IEcsRunSystem
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
                ref var ecsTransform = ref _filterTransforms.Get1(entityIndex);
                ref var transformRef = ref _filterTransforms.Get2(entityIndex);
                transformRef.Value.position = ecsTransform.Position;
                transformRef.Value.rotation = ecsTransform.Rotation;
                transformRef.Value.localScale = ecsTransform.Scale;
            }
            
            foreach (var entityIndex in _filterRectTransforms)
            {
                ref var ecsTransform = ref _filterRectTransforms.Get1(entityIndex);
                ref var rectTransformRef = ref _filterRectTransforms.Get2(entityIndex);
                rectTransformRef.Value.anchoredPosition = ecsTransform.Position;
                rectTransformRef.Value.rotation = ecsTransform.Rotation;
                rectTransformRef.Value.localScale = ecsTransform.Scale;
            }
            
            foreach (var entityIndex in _filterRigidbody)
            {
                ref var ecsTransform = ref _filterRigidbody.Get1(entityIndex);
                ref var rigidbodyRef = ref _filterRigidbody.Get2(entityIndex);
                rigidbodyRef.Value.position = ecsTransform.Position;
                rigidbodyRef.Value.rotation = ecsTransform.Rotation;
            }
            
            foreach (var entityIndex in _filterRigidbody2D)
            {
                ref var ecsTransform = ref _filterRigidbody2D.Get1(entityIndex);
                ref var rigidbodyRef = ref _filterRigidbody2D.Get2(entityIndex);
                rigidbodyRef.Value.position = ecsTransform.Position;
                rigidbodyRef.Value.rotation = ecsTransform.Rotation.eulerAngles.z;
            }
        }
    }
}
