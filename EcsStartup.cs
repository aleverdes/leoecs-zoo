using System;
using System.Collections;
using Leopotam.Ecs;
using UnityEngine;
namespace AffenCode
{
    public abstract class EcsStartup : MonoBehaviour
    {
        [SerializeField] protected EcsWorldProvider WorldProvider;

        protected EcsSystems FixedUpdateSystems;
        protected EcsSystems LateUpdateSystems;
        protected EcsSystems UpdateSystems;

        protected bool Initilized { get; private set; }
        
        private void Reset()
        {
            WorldProvider = FindObjectOfType<EcsWorldProvider>();
        }

        private void Start()
        {
            Initilized = true;
            
            SetupUpdateSystems();
            SetupFixedUpdateSystems();
            SetupLateUpdateSystems();
        }

        private void Update()
        {
            if (!Initilized)
            {
                return;
            }
            
            UpdateSystems?.Run();
        }

        private void FixedUpdate()
        {
            if (!Initilized)
            {
                return;
            }
            
            FixedUpdateSystems?.Run();
        }

        private void LateUpdate()
        {
            if (!Initilized)
            {
                return;
            }
            
            LateUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            if (UpdateSystems != null)
            {
                UpdateSystems.Destroy();
                UpdateSystems = null;
            }

            if (LateUpdateSystems != null)
            {
                LateUpdateSystems.Destroy();
                LateUpdateSystems = null;
            }

            if (FixedUpdateSystems != null)
            {
                FixedUpdateSystems.Destroy();
                FixedUpdateSystems = null;
            }
        }

        protected virtual EcsSystems SetupUpdateSystems()
        {
            UpdateSystems = new(WorldProvider.World);
            UpdateSystems.Add(new SyncFromUnityTransformSystem());
            AddUpdateSystems(UpdateSystems);
            UpdateSystems.Add(new SyncToUnityTransformSystem());
            AddUpdateOneFrameComponents(UpdateSystems);
            UpdateSystems.InjectData();
            UpdateSystems.Init();
            return UpdateSystems;
        }

        protected virtual EcsSystems SetupFixedUpdateSystems()
        {
            FixedUpdateSystems = new(WorldProvider.World);
            AddFixedUpdateSystems(FixedUpdateSystems);
            AddFixedUpdateOneFrameComponents(UpdateSystems);
            FixedUpdateSystems.InjectData();
            FixedUpdateSystems.Init();
            return FixedUpdateSystems;
        }

        protected virtual EcsSystems SetupLateUpdateSystems()
        {
            LateUpdateSystems = new(WorldProvider.World);
            LateUpdateSystems.Add(new SyncFromUnityTransformSystem());
            AddLateUpdateSystems(LateUpdateSystems);
            LateUpdateSystems.Add(new SyncToUnityTransformSystem());
            AddLateUpdateOneFrameComponents(UpdateSystems);
            LateUpdateSystems.InjectData();
            LateUpdateSystems.Init();
            return LateUpdateSystems;
        }

        protected abstract void AddUpdateSystems(EcsSystems ecsSystems);
        protected abstract void AddLateUpdateSystems(EcsSystems ecsSystems);
        protected abstract void AddFixedUpdateSystems(EcsSystems ecsSystems);

        protected virtual void AddUpdateOneFrameComponents(EcsSystems ecsSystems)
        {}
        protected virtual void AddFixedUpdateOneFrameComponents(EcsSystems ecsSystems)
        {}
        protected virtual void AddLateUpdateOneFrameComponents(EcsSystems ecsSystems)
        {}
    }
}
