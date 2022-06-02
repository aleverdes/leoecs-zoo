using UnityEngine;
namespace AffenCode
{
    public abstract class InjectableMonoBehaviour : MonoBehaviour
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
