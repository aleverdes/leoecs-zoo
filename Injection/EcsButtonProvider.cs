using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace AffenCode
{
    public abstract class EcsButtonProvider<T> : MonoBehaviour where T : struct
    {
        [FormerlySerializedAs("_worldProvider")] public EcsWorldProvider WorldProvider;
        [FormerlySerializedAs("_button")] public Button Button;
        [FormerlySerializedAs("_value")] public T Value;

        protected virtual void Reset()
        {
            WorldProvider = FindObjectOfType<EcsWorldProvider>();
            Button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            CreateClickEventEntity(WorldProvider);
        }

        protected virtual void CreateClickEventEntity(EcsWorldProvider ecsWorldProvider)
        {
            CreateClickEventComponent(ecsWorldProvider.World.NewEntity());
        }

        protected virtual void CreateClickEventComponent(EcsEntity ecsEntity)
        {
            ecsEntity.Replace(Value);
        }
    }
}
