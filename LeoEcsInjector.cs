using System.Collections.Generic;
using Leopotam.Ecs;

namespace AffenCode
{
    public static class LeoEcsInjector
    {
        private static readonly HashSet<object> InjectedObjects = new();
        
        public static void AddInjection(object injectedObject)
        {
            InjectedObjects.Add(injectedObject);
        }

        public static void RemoveInjection(object injectedObject)
        {
            InjectedObjects.Remove(injectedObject);
        }

        public static EcsSystems InjectData(this EcsSystems ecsSystems)
        {
            foreach (var injectedObject in InjectedObjects)
            {
                if (injectedObject != null)
                {
                    ecsSystems.Inject(injectedObject);
                }
            }

            return ecsSystems;
        }
    }
}