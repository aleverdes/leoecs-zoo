using Leopotam.Ecs;
namespace AffenCode
{
    public class SyncToUnityTransformSystem : IEcsPreInitSystem, IEcsRunSystem
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
                ref var ecsTransform = ref _filterTransforms.Get1(entityIndex);
                ref var unityTransform = ref _filterTransforms.Get2(entityIndex);
                unityTransform.Value.position = ecsTransform.Position;
                unityTransform.Value.rotation = ecsTransform.Rotation;
                unityTransform.Value.localScale = ecsTransform.Scale;
            }
            
            foreach (var entityIndex in _filterRigidbody)
            {
                ref var ecsTransform = ref _filterRigidbody.Get1(entityIndex);
                ref var rigidbodyRef = ref _filterRigidbody.Get2(entityIndex);
                rigidbodyRef.Value.position = ecsTransform.Position;
                rigidbodyRef.Value.rotation = ecsTransform.Rotation;
            }
        }
    }
}
