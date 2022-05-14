using Leopotam.Ecs;
namespace AffenCode
{
    public class SyncFromUnityTransformSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilter<EcsTransform, TransformRef>.Exclude<IgnoreTransformSync> _filterTransforms;
        private EcsFilter<EcsTransform, RigidbodyRef, IgnoreTransformSync>.Exclude<IgnoreRigidbodySync> _filterRigidbody;

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
            
            foreach (var entityIndex in _filterRigidbody)
            {
                ref var ecsTransform = ref _filterRigidbody.Get1(entityIndex);
                ref var rigidbodyRef = ref _filterRigidbody.Get2(entityIndex);
                ecsTransform.Position = rigidbodyRef.Value.position;
                ecsTransform.Rotation = rigidbodyRef.Value.rotation;
            }
        }
    }
}
