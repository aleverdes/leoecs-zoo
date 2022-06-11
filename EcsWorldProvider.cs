using Leopotam.Ecs;
using UnityEngine;

namespace AffenCode
{
    public class EcsWorldProvider : MonoBehaviour
    {
        public static EcsWorldProvider DefaultWorldProvider { get; private set; }

        /// <summary>
        /// World reference
        /// </summary>
        public EcsWorld World;
        
        /// <summary>
        /// World.Entities cache size.
        /// </summary>
        public int WorldEntitiesCacheSize = EcsWorldConfig.DefaultWorldEntitiesCacheSize;
        
        /// <summary>
        /// World.Filters cache size.
        /// </summary>
        public int WorldFiltersCacheSize = EcsWorldConfig.DefaultFilterEntitiesCacheSize;
        
        /// <summary>
        /// World.ComponentPools cache size.
        /// </summary>
        public int WorldComponentPoolsCacheSize = EcsWorldConfig.DefaultWorldEntitiesCacheSize;
        
        /// <summary>
        /// Entity.Components cache size (not doubled).
        /// </summary>
        public int EntityComponentsCacheSize = EcsWorldConfig.DefaultWorldFiltersCacheSize;
        
        /// <summary>
        /// Filter.Entities cache size.
        /// </summary>
        public int FilterEntitiesCacheSize = EcsWorldConfig.DefaultWorldComponentPoolsCacheSize;

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
            World = new EcsWorld(new EcsWorldConfig
            {
                WorldEntitiesCacheSize = WorldEntitiesCacheSize,
                WorldFiltersCacheSize = WorldFiltersCacheSize,
                WorldComponentPoolsCacheSize = WorldComponentPoolsCacheSize,
                EntityComponentsCacheSize = EntityComponentsCacheSize,
                FilterEntitiesCacheSize = FilterEntitiesCacheSize
            });
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