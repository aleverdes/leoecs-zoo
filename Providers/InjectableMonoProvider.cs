using UnityEngine;
namespace AffenCode
{
    public abstract class InjectableMonoProvider<T> : MonoProvider<T> where T : Component
    {
        private void Awake()
        {
            LeoEcsInjector.AddInjection(this);
        }

        private void OnDestroy()
        {
            LeoEcsInjector.RemoveInjection(this);
        }
    }
}
