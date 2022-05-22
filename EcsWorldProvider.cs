using Leopotam.Ecs;
using UnityEngine;

namespace AffenCode
{
    public class EcsWorldProvider : MonoBehaviour
    {
        public static EcsWorldProvider DefaultWorldProvider { get; private set; }
        
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
            if (!DefaultWorldProvider)
            {
                DefaultWorldProvider = this;
            }
        }

        public void DestroyWorld()
        {
            if (DefaultWorldProvider == this)
            {
                DefaultWorldProvider = null;
            }
            
            World?.Destroy();
            World = null;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetDefaultWorld()
        {
            DefaultWorldProvider = null;
        }
    }
}