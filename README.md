# Coorth.Framework

## 简介

一个独立于引擎的游戏开发框架，可用于客户端或者服务器。

## 设计

1. 基于**事件**逻辑驱动：Event-Dispatcher-Reaction
2. 基于**组件**结构组合：Entity-Component-System
3. 基于**消息**数据通信：Actor-Message-Proxy
4. 基于**节点**动态逻辑：Node-Graph-Process

## 范例

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

## 游戏世界

### 世界的构成：世界-实体-组件-系统

#### 世界(World)

每个World有自己的一套System集合，因而每个世界的运行逻辑是不同的。一个世界是Entity集合的边界，Entity不能跨越World，跨World传递Entity只能序列化反序列化。

```csharp
//创建世界
var options = new WorldOptions() {
    Name = "World-Sample",
    Services = Services,
    Dispatcher = Dispatcher.CreateChild(),
    Logger = new ConsoleLogger(),
};
var world = new World(options);

//销毁世界
world.Dispose();

```

#### 实体(Entity)

Entity结构非常简单，由EntityId和Sandbox的引用两部分组成；其中EntityId包含两部分：Index和Version。其中Index是复用的，每复用Version加1。

**实体结构：**

```csharp
// Entity 内存结构 
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

**实体操作：**

```csharp
//直接创建
var entity = world.CreateEntity();
entity.Add<PositionComponent>();
entity.Add<RotationComponent>();
entity.Add<VelocityComponent>();

//基于原型创建
var archetype = world.CreateArchetype()
                    .Add<PositionComponent>()
                    .Add<RotationComponent>()
                    .Add<VelocityComponent>()
                    .Compile();

var entity = archetype.CreateEntity();
var entities = archetype.CreateEntities(10);

//销毁实体
entity.Dispose()

//复制实体
var clone = entity.Clone();

```

#### 组件

组件系统采用了一种Archetype和Sparse Array的混合结构，来达到兼顾查询效率和缓存友好性的目的。

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

常见组件操作

```csharp
//定义struct组件
public struct PositionComponent : IComponent {
    public Vector3 Value;
}

//定义class组件
public class TransformComponent : RefComponent {
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scaling;
}

//添加组件
ref var component = ref entity.Add<PositionComponent>();
var component = entity.Add<PositionComponent>();
//添加组件：链式调用
entity.With<PositionComponent>().With<VelocityComponent>();
//检查组件是否存在
entity.Has<PositionComponent>();
//移除组件
entity.Remove<PositionComponent>();

//获取组件，组件不存在抛出异常
ref var component = ref entity.Get<PositionComponent>();
var component = entity.Get<PositionComponent>();

//获取组件，组件不存在创建组件
ref var position = ref entity.Offer<PositionComponent>();
var position = entity.Offer<PositionComponent>();

//获取组件：组件不存在返回false
var result = entity.TryGet<PositionComponent>(out var component);

//获取组件：组件不存在返回null
PositionComponent? position = entity.TryGet<PositionComponent>();

//清空组件
entity.Clear();

//单例组件
var component = world.Singleton<InputComponent>();
```

#### 系统

系统代表着游戏世界的运行规则，通常纯ECS架构中System中是不含逻辑的，System订阅某个事件，并在收到事件后对一组Component进行更新。

系统以树状结构组织

```csharp
// RootSystem -+- CharacterSystems -+- MovementSystem 
//             |                    +- CombatSystem
//             |
//             +- PlayerSystems ----+- CameraControlSystem
//                                  +- InputControlSystem
```

```csharp
//定义系统
public class TranslateSystem : SystemBase {

    protected override void OnActive() {
        Subscribte<TickUpdateEvent>().ForEach<VelocityComponent, PositionComponent>(OnExecute);
    }

    private void OnExecute(in TickUpdateEvent e, in VelocityComponent velocity, ref PositionComponent position) {
        position.Value = position.Value + velocity.Value * e.DeltaTime;
    }
}


//创建并添加系统
var movement_system = world.AddSystem<MovementSystem>();

//添加子系统(方式1)
public class MovementSystem : SystemBase {

    protected override void OnAdd() {
        AddSystem<TranslateSystem>();
        AddSystem<RotateSystem>();
    }
}
//添加子系统(方式2)
world.GetSystem<MovementSystem>().AddSystem<TranslateSystem>();
world.GetSystem<MovementSystem>().AddSystem<RotateSystem>();
```

### 世界的运转：事件-消息-分发-响应

只有Entity, Component, System构成的世界是静态的，为了让世界变化，需要对世界进行更新。通常在游戏中表现为各种Update和FixedUpdate函数，游戏事件，消息等等。在这里我们将其简化为事件和消息。

### 事件

```cs
//广播
world.Publish<LateUpdateEvent>(new LateUpdateEvent());
world.Publish<int>(12054);
world.Publish<string>("event_name");

//订阅
Subscribe<TickUpdateEvent>().OnEvent((in e) => { /**/ });

```
