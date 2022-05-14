using Leopotam.Ecs;
using UnityEngine;
namespace AffenCode
{
    public sealed class ConvertToEntity : MonoBehaviour
    {
        [SerializeField] private EcsWorldProvider _worldProvider;
        [SerializeField] private ConvertMode _convertMode;
        [SerializeField] private CollectMode _collectMode;

        private EcsEntity? _entity;

        private void Reset()
        {
            _worldProvider = FindObjectOfType<EcsWorldProvider>();
        }

        private void Start()
        {
            _entity = _worldProvider.World.NewEntity();

            var components = GetComponents<IConvertToEntity>();
            foreach (var component in components)
            {
                component.ConvertToEntity(_entity.Value);
            }

            if (_collectMode == CollectMode.IncludeChildren)
            {
                ConvertChildrenToEntity(transform);
            }

            if (_convertMode == ConvertMode.ConvertAndDestroy)
            {
                Destroy(gameObject);
            }
        }

        public bool TryGetEntity(out EcsEntity ecsEntity)
        {
            if (_entity.HasValue && _entity.Value.IsAlive())
            {
                ecsEntity = _entity.Value;
                return true;
            }

            ecsEntity = default;
            return false;
        }

        private void ConvertChildrenToEntity(Transform t)
        {
            for (var i = 0; i < transform.childCount; ++i)
            {
                var child = t.GetChild(i);
                if (!child.TryGetComponent<ConvertToEntity>(out var convertToEntity))
                {
                    var components = child.GetComponents<IConvertToEntity>();
                    foreach (var component in components)
                    {
                        component.ConvertToEntity(_entity.Value);
                    }
                    ConvertChildrenToEntity(t);
                }
            }
        }
    }
}
