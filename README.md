# LeoECS Zoo
Zoo of different utilities for LeoECS.
This repository provides a package of extensions and utilities for [LeoECS](https://github.com/Leopotam/ecs) (not LeoEcs Lite).

1. [Startup Template](#startup-template)
2. [Transform Workflow](#transform-workflow)
3. [Conversion Workflow](#conversion-workflow)
4. [Injection Workflow](#injection-workflow)

## Startup Template

The EcsStartup class offers a template for describing game systems for MonoBehavior Update methods (Update, FixedUpdate and LateUpdate), with built-in transform synchronization with Scene-objects and data auto-injection via LeoEcsInjector.

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

## Transform Workflow

### EcsTransform

A built-in component for describing the position and rotation of an object in world coordinates, and automatically synchronized with Scene-objects if the same Entity has TransformRef or RigidbodyRef components and uses the EcsStartup template.

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

## Conversion Workflow

### Manual conversion

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

### Component Conversion

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

## Injection Workflow

### MonoBehaviour auto-injection

Injectable MonoBehaviour is a MonoBehaviour class for automatically injecting a component into all EcsSystems in the game.

```csharp
using AffenCode;
using UnityEngine;

public class LayerMaskManager : InjectableMonoBehaviour
{
    public LayerMask Level;
    public LayerMask Player;
    public LayerMask Enemy;
}
```

### Manual object injection

If it is not possible to inherit a component from InjectableMonoBehaviour, you can manually register and remove automatic injection into the system using methods `LeoEcsInjector.AddInjection(this)` and `LeoEcsInjector.RemoveInjection(this)`.
```csharp
using AffenCode;
using UnityEngine;

public class LayerMaskManager : MonoBehaviour
{
    public LayerMask Level;
    public LayerMask Player;
    public LayerMask Enemy;
    
    private void Awake()
    {
        LeoEcsInjector.AddInjection(this);
    }

    private void OnDestroy()
    {
        LeoEcsInjector.RemoveInjection(this);
    }
}
```

A similar method can be used for regular classes (not MonoBehaviour descendants).

```csharp
using AffenCode;
using UnityEngine;

public class LayerMaskManager
{
    public LayerMask Level;
    public LayerMask Player;
    public LayerMask Enemy;
    
    public LayerMaskManager()
    {
        LeoEcsInjector.AddInjection(this);
    }

    ~LayerMaskManager()
    {
        LeoEcsInjector.RemoveInjection(this);
    }
}
```

### Usage

```csharp
using Leopotam.Ecs;
using AffenCode;

public class PlayerCollectSystem : IEcsRunSystem
{
    private LayerMaskManager _layerMaskManager; // auto injected
    
    private EcsFilter<EcsTransform> _filter;

    public void Run()
    {
        foreach (var entityIndex in _filter)
        {
            var entity = _filter.GetEntity(entityIndex);
            ref var transform = ref _filter.Get1(entityIndex);

            // ..._layerMaskManager.Enemy...
        }
    }
}
```

### Usage Without EcsStartup-template

If you are not using EcsStartup-template, then you need to call the Extension method for EcsSystems `InjectData()`

```csharp
FixedUpdateSystems = new(WorldProvider.World);
FixedUpdateSystems
    .Add(new PlayerMovementSystem())
    .InjectData() // this
    .Init();
```

