using System;
using Leopotam.Ecs;
using UnityEngine;
namespace AffenCode
{
    [RequireComponent(typeof(ConvertToEntity))]
    public class UnityObjectConvertToEntity : MonoBehaviour, IConvertToEntity
    {
        public void ConvertToEntity(EcsEntity ecsEntity)
        {
            ecsEntity.AddGameObject(gameObject);

            if (transform is RectTransform rectTransform)
            {
                ecsEntity.AddRectTransform(rectTransform);
            }
            else
            {
                ecsEntity.AddTransform(transform);
            }
            
            if (TryGetComponent<Rigidbody>(out var rb))
            {
                ecsEntity.AddRigidbody(rb);
            }
            
            if (TryGetComponent<Rigidbody2D>(out var rb2d))
            {
                ecsEntity.AddRigidbody2D(rb2d);
            }
        }
    }
}
