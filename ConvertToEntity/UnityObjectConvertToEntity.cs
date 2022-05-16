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
            ecsEntity.AddTransform(transform);
            if (TryGetComponent<Rigidbody>(out var rb))
            {
                ecsEntity.AddRigidbody(rb);
            }
        }
    }
}
