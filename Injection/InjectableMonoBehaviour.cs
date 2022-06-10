using UnityEngine;
namespace AffenCode
{
    public abstract class InjectableMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            LeoEcsInjector.AddInjection(this);
        }

        protected virtual  void OnDestroy()
        {
            LeoEcsInjector.RemoveInjection(this);
        }
    }
}
