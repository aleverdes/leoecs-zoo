using Leopotam.Ecs;
using UnityEngine;
namespace AffenCode
{
    public class EntityCollision : MonoBehaviour, IConvertToEntity
    {
        private EcsEntity _entity;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _entity.Replace(new CollisionEnter2D
            {
                Collision = collision
            });
        }

        private void OnCollisionEnter(Collision collision)
        {
            _entity.Replace(new CollisionEnter
            {
                Collision = collision
            });
        }

        public void ConvertToEntity(EcsEntity ecsEntity)
        {
            _entity = ecsEntity;
        }
    }
}
