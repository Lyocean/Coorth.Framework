# Coorth.Framework

## Introduction

An engine-independent game development framework, can be used for game client or server.

## Design

1. **Event**: Event-Dispatcher-Reaction
2. **Component**: Entity-Component-System
3. **Message** : Actor-Message-Proxy
4. **Node**: Node-Graph-Process

## Example

```csharp

public struct VelocityComponent : IComponent {
    public Vector3 Value;
}

public struct PositionComponent : IComponent {
    public Vector3 Value;
}

public class MovementSystem : SystemBase {

    protected override OnActive() {
        Subscribe<TickUpdateEvent>().ForEach<VelocityComponent, PositionComponent>(OnTickUpdate);
    }
    
    private void OnTickUpdate(in TickUpdateEvent e, in VelocityComponent velocity_comp, ref PositionComponent position_comp) {
        position_comp.Value += velocity_comp.Value * e.DeltaSeconds;
    }
}

```

## World

Each `world` has its own set of System collections, and thus each `world` operates with a different logic. A `world` is the boundary of an Entity collection, Entities cannot cross Worlds, and passing Entities across Worlds can only be serialized and deserialize.

```csharp
// Create Worlds
var options = new WorldOptions() {
    Name = "Sandbox-Sample",
    Services = Services,
    Dispatcher = Dispatcher.CreateChild(),
    Logger = new ConsoleLogger(),
};
var world = new World(options);

// Destroy the world
world.Dispose();

```

#### Entity

`Entity` structure is very simple, consisting of two parts: EntityId and `world` reference; where `EntityId` contains two parts: index and version. where index is multiplexed, and each multiplexed Version plus 1.

**Entity structure:**

```csharp
// Entity memory structure 
// +-----------------+---------+
// |    EntityId     |         |
// +-------+---------+  World  |
// | Index | Version |         |
// +-------+---------+---------+
// |  Int  |   Int   |   Ref   |
// +-------+---------+---------+

public readonly record struct EntityId {
    public readonly int Index;
    public readonly int Version;
}

public readonly record struct Entity {
    public readonly EntityId Id;
    public readonly World World;
}
```

**Entity operations:**

```csharp
// Create directly
var entity = world.CreateEntity();
entity.Add<PositionComponent>();
entity.Add<RotationComponent>();
entity.Add<VelocityComponent>();

//create based on prototype
var archetype = world.CreateArchetype()
                    .Add<PositionComponent>()
                    .Add<RotationComponent>()
                    .Add<VelocityComponent>()
                    .Compile();

var entity = archetype.CreateEntity();
var entities = archetype.CreateEntities(10);

// Destroy the entity
Dispose()

//Clone the entity
var clone = entity.Clone();

```

#### Components

The component uses a hybrid structure of archetype and sparse array to achieve a balance of query efficiency and cache friendly.

```csharp
// Component Chunk Array
// +---------------+    +---------------+
// |   CompChunk   | -> |   CompChunk   | -> ...
// +---------------+    +---------------+


// Component Chunk

// Component Array
// +---------------+---------------+---------------+---------------+
// |   Component   |   Component   |   Component   |   Component   |
// +---------------+---------------+---------------+---------------+
// Dense Array
// +---------------+---------------+---------------+---------------+
// | Entity Index  | Entity Index  | Entity Index  | Entity Index  |
// +---------------+---------------+---------------+---------------+

```

Common component operations


```csharp
public struct PositionComponent : IComponent {
    public Vector3 Value;
}

public class TransformComponent : RefComponent {
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scaling;
}

//Add component
ref var component = ref entity.Add<PositionComponent>();
var component = entity.Add<PositionComponent>();

//Add component: fluent invoke
entity.With<PositionComponent>().With<VelocityComponent>();

//Check if the component exists
entity.Has<PositionComponent>();

//Remove the component
entity.Remove<PositionComponent>();

//Get component, throw exception when component does not exist
ref var component = ref entity.Get<PositionComponent>();
var component = entity.Get<PositionComponent>();

//Get component, create component when component does not exist 
ref var position = ref entity.Offer<PositionComponent>();
var position = entity.Offer<PositionComponent>();

//Get component: return false if component does not exist
var result = entity.TryGet<PositionComponent>(out var component);

//Get component: return null when component does not exist 
PositionComponent? position = entity.TryGet<PositionComponent>();

//Clear all components
entity.Clear();

// Singleton component
var component = world.Singleton<InputComponent>();
```

#### System

System represents the rules for running the game world. Usually there is no logic in System in pure ECS architecture, system subscribes some event and updates a set of components upon receipt of the event.

System is organized in a tree-liked structure

```csharp
// RootSystem -+- CharacterSystems -+- MovementSystem 
//             |                    +- CombatSystem
//             |
//             +- PlayerSystems ----+- CameraControlSystem
//                                  +- InputControlSystem
```

```csharp
//Define the system
public class TranslateSystem : SystemBase {

    protected override void OnActive() {
        Subscribte<TickUpdateEvent>().ForEach<VelocityComponent, PositionComponent>(OnExecute);
    }

    private void OnExecute(in TickUpdateEvent e, in VelocityComponent velocity, ref PositionComponent position) {
        position.Value = position.Value + velocity.Value * e.DeltaTime;
    }
}


//Create and add a system
var movement_system = world.AddSystem<MovementSystem>();

//add subsystem
public class MovementSystem : SystemBase {

    protected override void OnAdd() {
        AddSystem<TranslateSystem>();
        AddSystem<RotateSystem>();
    }
}
//Add a subsystem 
world.GetSystem<MovementSystem>().AddSystem<TranslateSystem>();
world.GetSystem<MovementSystem>().AddSystem<RotateSystem>();
```

### The world works: event-message-distribution-response

The world consisting of only Entity, Component, and System is static and needs to be updated in order for the world to change. This is usually expressed in the game as various Update and FixedUpdate functions, game events, messages, etc. Here we will simplify it to events and messages.

### Events

```csharp
// Broadcast
world.Publish<LateUpdateEvent>(new LateUpdateEvent());
world.Publish<int>(12054);
world.Publish<string>("event_name");

//Subscribe
Subscribe<TickUpdateEvent>().OnEvent((in e) => { /**/ });

```

