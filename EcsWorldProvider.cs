using Leopotam.Ecs;
using UnityEngine;

namespace AffenCode
{
    public class EcsWorldProvider : MonoBehaviour
    {
        public EcsWorld World;

        protected virtual void Awake()
        {
            CreateWorld();
        }

        protected virtual void OnDestroy()
        {
            DestroyWorld();
        }

        public void CreateWorld()
        {
            World = new EcsWorld();
        }

        public void DestroyWorld()
        {
            World?.Destroy();
            World = null;
        }
    }
}