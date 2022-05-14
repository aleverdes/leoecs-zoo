# LeoECS Zoo
Zoo of different utilities for LeoECS

## Usage

### Startup Template

```csharp
using Leopotam.Ecs;
using AffenCode;

public class GameEcsStartup : EcsStartup
{
    protected override void AddUpdateSystems(EcsSystems ecsSystems)
    {
        ecsSystems
            // your update systems
            ;
    }

    protected override void AddLateUpdateSystems(EcsSystems ecsSystems)
    {
        ecsSystems
            // your late update systems
            ;
    }

    protected override void AddFixedUpdateSystems(EcsSystems ecsSystems)
    {
        ecsSystems
            // your fixed update systems
            ;
    }
}
```

### EcsTransform

```csharp
using Leopotam.Ecs;
using AffenCode;
using UnityEngine;

public class PlayerMovementSystem : IEcsRunSystem
{
    private EcsFilter<EcsTransform> _transforms;

    public void Run()
    {
        foreach (var entityIndex in _transforms)
        {
            ref var ecsTransform = ref _transforms.Get1(playentityIndexerIndex);
            ecsTransform.Position += Vector3.forward * Time.deltaTime;
        }
    }
}
```