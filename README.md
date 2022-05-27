# Coorth.Framework

## 注意

目前施工中。系统尚未完成。

## 简介

一个独立于引擎的游戏开发框架

## 设计

1. 基于**事件**逻辑驱动：Event-Dispatcher-Reaction
2. 基于**组件**结构组合：Entity-Component-System
3. 基于**消息**数据通信：Actor-Message-Proxy
4. 基于**节点**动态逻辑：Node-Graph-Process

## Example``

```csharp

public struct VelocityComponent : IComponent {
    public Vector3 Value;
}

public struct PositionComponent : IComponent {
    public Vector3 Value;
}

public class MovementSystem : IComponent {
    protected override OnActive() {
        Subscribe<EventTickUpdate>().ForEach<VelocityComponent, PositionComponent>()
    }
    
    private void OnTickUpdate(in EventTickUpdate e, in VelocityComponent velocity_comp, ref PositionComponent position_comp){
        position_comp.Value += velocity_comp.Value * e.DeltaSeconds;
    }
}

```