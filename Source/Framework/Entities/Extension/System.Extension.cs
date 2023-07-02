
using System;
using System.Runtime.CompilerServices;
using Coorth.Logs;

namespace Coorth.Framework;

public partial class SystemSubscription<TEvent> {

    public void ForEach<T0, T1>(ActionI2R2<TEvent, Entity, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1>(ActionR2<T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1) => action(ref component0, ref component1));        
    }

    public void ForEach<T0, T1>(ActionI1R2<Entity, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1) => action(in entity, ref component0, ref component1));        
    }

    public void ForEach<T0, T1>(ActionI1R2<TEvent, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1) => action(in e, ref component0, ref component1));        
    }

    public void ForEach<T0, T1>(Action<T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((ref T0 component0, ref T1 component1) => action(component0, component1));
    }

    public void ForEach<T0, T1>(Action<Entity, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1) => action(entity, component0, component1));
    }

    public void ForEach<T0, T1>(Action<TEvent, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1) => action(e, component0, component1));
    }

    public void ForEach<T0, T1>(Action<TEvent, Entity, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1) => action(e, entity, component0, component1));
    }

    public void ForEach<T0, T1>(ActionI1R1<T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1) => action(in component0, ref component1));
    }

    public void ForEach<T0, T1>(ActionI2R1<Entity, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1) => action(in entity, in component0, ref component1));
    }

    public void ForEach<T0, T1>(ActionI2R1<TEvent, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1) => action(in e, in component0, ref component1));
    }

    public void ForEach<T0, T1>(ActionI3R1<TEvent, Entity, T0, T1> action) where T0 : IComponent where T1 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1) => action(in e, in entity, in component0, ref component1));
    }


    public void ForEach<T0, T1, T2>(ActionI2R3<TEvent, Entity, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1, T2>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1, T2>(ActionR3<T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2) => action(ref component0, ref component1, ref component2));        
    }

    public void ForEach<T0, T1, T2>(ActionI1R3<Entity, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2) => action(in entity, ref component0, ref component1, ref component2));        
    }

    public void ForEach<T0, T1, T2>(ActionI1R3<TEvent, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2) => action(in e, ref component0, ref component1, ref component2));        
    }

    public void ForEach<T0, T1, T2>(Action<T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((ref T0 component0, ref T1 component1, ref T2 component2) => action(component0, component1, component2));
    }

    public void ForEach<T0, T1, T2>(Action<Entity, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2) => action(entity, component0, component1, component2));
    }

    public void ForEach<T0, T1, T2>(Action<TEvent, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1, ref T2 component2) => action(e, component0, component1, component2));
    }

    public void ForEach<T0, T1, T2>(Action<TEvent, Entity, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2) => action(e, entity, component0, component1, component2));
    }

    public void ForEach<T0, T1, T2>(ActionI1R2<T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2) => action(in component0, ref component1, ref component2));
    }

    public void ForEach<T0, T1, T2>(ActionI2R2<Entity, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2) => action(in entity, in component0, ref component1, ref component2));
    }

    public void ForEach<T0, T1, T2>(ActionI2R2<TEvent, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2) => action(in e, in component0, ref component1, ref component2));
    }

    public void ForEach<T0, T1, T2>(ActionI3R2<TEvent, Entity, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2) => action(in e, in entity, in component0, ref component1, ref component2));
    }

    public void ForEach<T0, T1, T2>(ActionI2R1<T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2) => action(in component0, in component1, ref component2));
    }

    public void ForEach<T0, T1, T2>(ActionI3R1<Entity, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2) => action(in entity, in component0, in component1, ref component2));
    }

    public void ForEach<T0, T1, T2>(ActionI3R1<TEvent, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2) => action(in e, in component0, in component1, ref component2));
    }

    public void ForEach<T0, T1, T2>(ActionI4R1<TEvent, Entity, T0, T1, T2> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2) => action(in e, in entity, in component0, in component1, ref component2));
    }


    public void ForEach<T0, T1, T2, T3>(ActionI2R4<TEvent, Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1, T2, T3>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1, T2, T3>(ActionR4<T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(ref component0, ref component1, ref component2, ref component3));        
    }

    public void ForEach<T0, T1, T2, T3>(ActionI1R4<Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in entity, ref component0, ref component1, ref component2, ref component3));        
    }

    public void ForEach<T0, T1, T2, T3>(ActionI1R4<TEvent, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in e, ref component0, ref component1, ref component2, ref component3));        
    }

    public void ForEach<T0, T1, T2, T3>(Action<T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(component0, component1, component2, component3));
    }

    public void ForEach<T0, T1, T2, T3>(Action<Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(entity, component0, component1, component2, component3));
    }

    public void ForEach<T0, T1, T2, T3>(Action<TEvent, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(e, component0, component1, component2, component3));
    }

    public void ForEach<T0, T1, T2, T3>(Action<TEvent, Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(e, entity, component0, component1, component2, component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI1R3<T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in component0, ref component1, ref component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI2R3<Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in entity, in component0, ref component1, ref component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI2R3<TEvent, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in e, in component0, ref component1, ref component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI3R3<TEvent, Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in e, in entity, in component0, ref component1, ref component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI2R2<T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in component0, in component1, ref component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI3R2<Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in entity, in component0, in component1, ref component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI3R2<TEvent, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in e, in component0, in component1, ref component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI4R2<TEvent, Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in e, in entity, in component0, in component1, ref component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI3R1<T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in component0, in component1, in component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI4R1<Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in entity, in component0, in component1, in component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI4R1<TEvent, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in e, in component0, in component1, in component2, ref component3));
    }

    public void ForEach<T0, T1, T2, T3>(ActionI5R1<TEvent, Entity, T0, T1, T2, T3> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3) => action(in e, in entity, in component0, in component1, in component2, ref component3));
    }


    public void ForEach<T0, T1, T2, T3, T4>(ActionI2R5<TEvent, Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1, T2, T3, T4>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionR5<T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(ref component0, ref component1, ref component2, ref component3, ref component4));        
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI1R5<Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in entity, ref component0, ref component1, ref component2, ref component3, ref component4));        
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI1R5<TEvent, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, ref component0, ref component1, ref component2, ref component3, ref component4));        
    }

    public void ForEach<T0, T1, T2, T3, T4>(Action<T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(component0, component1, component2, component3, component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(Action<Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(entity, component0, component1, component2, component3, component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(Action<TEvent, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(e, component0, component1, component2, component3, component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(Action<TEvent, Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(e, entity, component0, component1, component2, component3, component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI1R4<T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in component0, ref component1, ref component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI2R4<Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in entity, in component0, ref component1, ref component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI2R4<TEvent, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, in component0, ref component1, ref component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI3R4<TEvent, Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, in entity, in component0, ref component1, ref component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI2R3<T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in component0, in component1, ref component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI3R3<Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in entity, in component0, in component1, ref component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI3R3<TEvent, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, in component0, in component1, ref component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI4R3<TEvent, Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, in entity, in component0, in component1, ref component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI3R2<T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in component0, in component1, in component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI4R2<Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in entity, in component0, in component1, in component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI4R2<TEvent, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, in component0, in component1, in component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI5R2<TEvent, Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, in entity, in component0, in component1, in component2, ref component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI4R1<T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in component0, in component1, in component2, in component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI5R1<Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in entity, in component0, in component1, in component2, in component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI5R1<TEvent, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, in component0, in component1, in component2, in component3, ref component4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(ActionI6R1<TEvent, Entity, T0, T1, T2, T3, T4> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4) => action(in e, in entity, in component0, in component1, in component2, in component3, ref component4));
    }


    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI2R6<TEvent, Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1, T2, T3, T4, T5>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionR6<T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI1R6<Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI1R6<TEvent, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(Action<T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(component0, component1, component2, component3, component4, component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(Action<Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(entity, component0, component1, component2, component3, component4, component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(Action<TEvent, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(e, component0, component1, component2, component3, component4, component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(Action<TEvent, Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(e, entity, component0, component1, component2, component3, component4, component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI1R5<T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in component0, ref component1, ref component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI2R5<Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI2R5<TEvent, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in component0, ref component1, ref component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI3R5<TEvent, Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI2R4<T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in component0, in component1, ref component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI3R4<Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI3R4<TEvent, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in component0, in component1, ref component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI4R4<TEvent, Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI3R3<T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in component0, in component1, in component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI4R3<Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI4R3<TEvent, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in component0, in component1, in component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI5R3<TEvent, Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI4R2<T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in component0, in component1, in component2, in component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI5R2<Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in entity, in component0, in component1, in component2, in component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI5R2<TEvent, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in component0, in component1, in component2, in component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI6R2<TEvent, Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in entity, in component0, in component1, in component2, in component3, ref component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI5R1<T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in component0, in component1, in component2, in component3, in component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI6R1<Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in entity, in component0, in component1, in component2, in component3, in component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI6R1<TEvent, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in component0, in component1, in component2, in component3, in component4, ref component5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(ActionI7R1<TEvent, Entity, T0, T1, T2, T3, T4, T5> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, ref component5));
    }


    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI2R7<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1, T2, T3, T4, T5, T6>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionR7<T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI1R7<Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI1R7<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(Action<T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(component0, component1, component2, component3, component4, component5, component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(Action<Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(entity, component0, component1, component2, component3, component4, component5, component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(Action<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(e, component0, component1, component2, component3, component4, component5, component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(Action<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(e, entity, component0, component1, component2, component3, component4, component5, component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI1R6<T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI2R6<Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI2R6<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI3R6<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI2R5<T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI3R5<Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI3R5<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI4R5<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI3R4<T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI4R4<Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI4R4<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI5R4<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI4R3<T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI5R3<Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in entity, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI5R3<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI6R3<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in entity, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI5R2<T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in component0, in component1, in component2, in component3, in component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI6R2<Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in entity, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI6R2<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI7R2<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI6R1<T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in component0, in component1, in component2, in component3, in component4, in component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI7R1<Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI7R1<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, ref component6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(ActionI8R1<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, ref component6));
    }


    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI2R8<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1, T2, T3, T4, T5, T6, T7>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionR8<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI1R8<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI1R8<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(Action<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(component0, component1, component2, component3, component4, component5, component6, component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(Action<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(entity, component0, component1, component2, component3, component4, component5, component6, component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(Action<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(e, component0, component1, component2, component3, component4, component5, component6, component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(Action<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(e, entity, component0, component1, component2, component3, component4, component5, component6, component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI1R7<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI2R7<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI2R7<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI3R7<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI2R6<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI3R6<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI3R6<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI4R6<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI3R5<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI4R5<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI4R5<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI5R5<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI4R4<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI5R4<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in entity, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI5R4<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI6R4<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in entity, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI5R3<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI6R3<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in entity, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI6R3<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI7R3<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI6R2<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI7R2<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI7R2<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI8R2<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI7R1<T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI8R1<Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI8R1<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7>(ActionI9R1<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7));
    }


    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI2R9<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1, T2, T3, T4, T5, T6, T7, T8>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionR9<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI1R9<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI1R9<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(component0, component1, component2, component3, component4, component5, component6, component7, component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Action<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(entity, component0, component1, component2, component3, component4, component5, component6, component7, component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Action<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(e, component0, component1, component2, component3, component4, component5, component6, component7, component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Action<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(e, entity, component0, component1, component2, component3, component4, component5, component6, component7, component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI1R8<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI2R8<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI2R8<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI3R8<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI2R7<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI3R7<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI3R7<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI4R7<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI3R6<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI4R6<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI4R6<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI5R6<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI4R5<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI5R5<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI5R5<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI6R5<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in entity, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI5R4<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI6R4<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI6R4<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI7R4<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI6R3<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI7R3<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI7R3<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI8R3<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI7R2<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI8R2<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI8R2<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI9R2<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI8R1<T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI9R1<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI9R1<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, ref component8));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI10R1<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, ref component8));
    }


    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI2R10<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        var matcher = new Matcher().WithAll<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        _OnMatch(matcher);
        OnEvent((in TEvent e) => {
            var query = World.Query(matcher);
            query.ForEach(in e, action);
        });
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionR10<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI1R10<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI1R10<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, ref component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));        
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(component0, component1, component2, component3, component4, component5, component6, component7, component8, component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(entity, component0, component1, component2, component3, component4, component5, component6, component7, component8, component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(e, component0, component1, component2, component3, component4, component5, component6, component7, component8, component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(e, entity, component0, component1, component2, component3, component4, component5, component6, component7, component8, component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI1R9<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI2R9<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI2R9<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI3R9<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, ref component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI2R8<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI3R8<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI3R8<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI4R8<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, in component1, ref component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI3R7<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI4R7<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI4R7<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI5R7<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, in component1, in component2, ref component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI4R6<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI5R6<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI5R6<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI6R6<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, in component1, in component2, in component3, ref component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI5R5<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI6R5<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI6R5<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI7R5<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, ref component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI6R4<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI7R4<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI7R4<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI8R4<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, ref component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI7R3<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI8R3<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI8R3<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI9R3<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, ref component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI8R2<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI9R2<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI9R2<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI10R2<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, ref component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI9R1<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, in component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI10R1<Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent _, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, in component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI10R1<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity _, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, in component8, ref component9));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI11R1<TEvent, Entity, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        ForEach((in TEvent e, in Entity entity, ref T0 component0, ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6, ref T7 component7, ref T8 component8, ref T9 component9) => action(in e, in entity, in component0, in component1, in component2, in component3, in component4, in component5, in component6, in component7, in component8, ref component9));
    }

}

public partial class SystemBase {

    #region ForEach

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0>(ActionI1R1<TEvent, T0> action) where TEvent : notnull where T0 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1>(ActionI1R2<TEvent, T0, T1> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1, T2>(ActionI1R3<TEvent, T0, T1, T2> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1, T2, T3>(ActionI1R4<TEvent, T0, T1, T2, T3> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1, T2, T3, T4>(ActionI1R5<TEvent, T0, T1, T2, T3, T4> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1, T2, T3, T4, T5>(ActionI1R6<TEvent, T0, T1, T2, T3, T4, T5> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1, T2, T3, T4, T5, T6>(ActionI1R7<TEvent, T0, T1, T2, T3, T4, T5, T6> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1, T2, T3, T4, T5, T6, T7>(ActionI1R8<TEvent, T0, T1, T2, T3, T4, T5, T6, T7> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8>(ActionI1R9<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void ForEach<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ActionI1R10<TEvent, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where TEvent : notnull where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        Subscribe<TEvent>().ForEach(action);
    }

    #endregion
}
