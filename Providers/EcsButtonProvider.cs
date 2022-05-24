using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
namespace AffenCode
{
    public abstract class EcsButtonProvider<T> : MonoBehaviour where T : struct
    {
        [SerializeField] private EcsWorldProvider _worldProvider;
        [SerializeField] private Button _button;
        [SerializeField] private T _value;

        private void Reset()
        {
            _worldProvider = FindObjectOfType<EcsWorldProvider>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            CreateClickEventEntity(_worldProvider);
        }

        protected virtual void CreateClickEventEntity(EcsWorldProvider ecsWorldProvider)
        {
            CreateClickEventComponent(ecsWorldProvider.World.NewEntity());
        }

        protected virtual void CreateClickEventComponent(EcsEntity ecsEntity)
        {
            ecsEntity.Replace(_value);
        }
    }
}
