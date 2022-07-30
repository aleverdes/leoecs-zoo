using UnityEngine;
namespace AffenCode
{
    public sealed class InjectObject : MonoBehaviour
    {
        public Object Injectable;
        
        private void Awake()
        {
            LeoEcsInjector.AddInjection(Injectable);
        }

        private void OnDestroy()
        {
            LeoEcsInjector.RemoveInjection(Injectable);
        }
    }
}
