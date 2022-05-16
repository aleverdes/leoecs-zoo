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

### Convert To Entity

#### Manual conversion

```csharp
using Leopotam.Ecs;
using AffenCode;
using UnityEngine;

[RequireComponent(typeof(ConvertToEntity))]
public class BasicConvertToEntity : MonoBehaviour, IConvertToEntity
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
```

#### Component Conversion

```csharp
using AffenCode;

public class PlayerConvertToEntity : ConvertComponent<Player>
{
}
```

```csharp
using System;

[Serializable] // Required for ConvertComponent<T>
public struct Player
{
    public string Name;
}
```