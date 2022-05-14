using Leopotam.Ecs;
using UnityEngine;
namespace AffenCode
{
    public abstract class ConvertComponent<T> : MonoBehaviour, IConvertToEntity where T : struct
    {
        public T Value;
        
        public void ConvertToEntity(EcsEntity ecsEntity)
        {
            ecsEntity.Replace(Value);
        }
    }
}
