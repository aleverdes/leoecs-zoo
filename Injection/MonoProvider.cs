using UnityEngine;

namespace AffenCode
{
    public abstract class MonoProvider<T> : MonoBehaviour where T : Component
    {
        public T Value;
    
        protected virtual void Reset()
        {
            Value = GetComponent<T>();
        }
    }
}