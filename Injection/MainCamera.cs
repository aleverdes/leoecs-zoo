using UnityEngine;
namespace AffenCode
{
    public class MainCamera : InjectableMonoBehaviour
    {
        public Camera Value;

        protected override void Awake()
        {
            base.Awake();
            LeoEcsInjector.AddInjection(Value);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LeoEcsInjector.RemoveInjection(Value);
        }
    }
}
