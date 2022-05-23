namespace Coorth.Framework; 

public static class EntityExtension {

    #region Add

    public static (T1, T2) Add<T1, T2>(this Entity entity) where T1 : IComponent, new()
        where T2 : IComponent, new() {
        var c1 = entity.Add<T1>();
        var c2 = entity.Add<T2>();
        return (c1, c2);
    }

    public static (T1, T2, T3) Add<T1, T2, T3>(this Entity entity) where T1 : IComponent, new()
        where T2 : IComponent, new()
        where T3 : IComponent, new() {
        var c1 = entity.Add<T1>();
        var c2 = entity.Add<T2>();
        var c3 = entity.Add<T3>();
        return (c1, c2, c3);
    }

    public static (T1, T2, T3, T4) Add<T1, T2, T3, T4>(this Entity entity) where T1 : IComponent, new()
        where T2 : IComponent, new()
        where T3 : IComponent, new()
        where T4 : IComponent, new() {
        var c1 = entity.Add<T1>();
        var c2 = entity.Add<T2>();
        var c3 = entity.Add<T3>();
        var c4 = entity.Add<T4>();
        return (c1, c2, c3, c4);
    }

    public static (T1, T2, T3, T4, T5) Add<T1, T2, T3, T4, T5>(this Entity entity) where T1 : IComponent, new()
        where T2 : IComponent, new()
        where T3 : IComponent, new()
        where T4 : IComponent, new()
        where T5 : IComponent, new() {
        var c1 = entity.Add<T1>();
        var c2 = entity.Add<T2>();
        var c3 = entity.Add<T3>();
        var c4 = entity.Add<T4>();
        var c5 = entity.Add<T5>();
        return (c1, c2, c3, c4, c5);
    }

    #endregion

    #region Has

    public static bool Has<T1, T2>(this Entity entity) where T1 : IComponent
        where T2 : IComponent {
        return entity.Has<T1>() && entity.Has<T2>();
    }

    public static bool Has<T1, T2, T3>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent {
        return entity.Has<T1>() && entity.Has<T2>() && entity.Has<T3>();
    }

    public static bool Has<T1, T2, T3, T4>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent {
        return entity.Has<T1>() && entity.Has<T2>() && entity.Has<T3>() && entity.Has<T4>();
    }

    public static bool Has<T1, T2, T3, T4, T5>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
        where T5 : IComponent {
        return entity.Has<T1>() && entity.Has<T2>() && entity.Has<T3>() && entity.Has<T4>() && entity.Has<T5>();
    }

    #endregion

    #region Get

    public static (T1, T2) Get<T1, T2>(this Entity entity) where T1 : IComponent
        where T2 : IComponent {
        return (entity.Get<T1>(), entity.Get<T2>());
    }

    public static (T1, T2, T3) Get<T1, T2, T3>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent {
        return (entity.Get<T1>(), entity.Get<T2>(), entity.Get<T3>());
    }

    public static (T1, T2, T3, T4) Get<T1, T2, T3, T4>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent {
        return (entity.Get<T1>(), entity.Get<T2>(), entity.Get<T3>(), entity.Get<T4>());
    }

    public static (T1, T2, T3, T4, T5) Get<T1, T2, T3, T4, T5>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
        where T5 : IComponent {
        return (entity.Get<T1>(), entity.Get<T2>(), entity.Get<T3>(), entity.Get<T4>(), entity.Get<T5>());
    }

    #endregion

    #region Set

    public static ref T Set<T>(this Entity entity) where T : IComponent, new() {
        return ref entity.Offer<T>();
    }
        
    public static ref T Set<T>(this Entity entity, T defaultValue) where T : IComponent, new() {
        if (entity.Has<T>()) {
            entity.Modify(defaultValue);
        }
        else {
            entity.Add(defaultValue);
        }
        return ref entity.Get<T>();
    }

    #endregion
        
    #region Find

#nullable enable
        
    public static T? Find<T>(this Entity entity) where T : class, IComponent {
        return entity.TryGet<T>(out var component) ? component : null;
    }
        
#nullable disable

    #endregion
        
    #region Remove

    public static bool Remove<T1, T2>(this Entity entity) where T1 : IComponent
        where T2 : IComponent {
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        return result1 && result2;
    }

    public static bool Remove<T1, T2, T3>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent {
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        return result1 && result2 && result3;
    }

    public static bool Remove<T1, T2, T3, T4>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent {
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        var result4 = entity.Remove<T4>();
        return result1 && result2 && result3 && result4;
    }

    public static bool Remove<T1, T2, T3, T4, T5>(this Entity entity) where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
        where T5 : IComponent {
        var result1 = entity.Remove<T1>();
        var result2 = entity.Remove<T2>();
        var result3 = entity.Remove<T3>();
        var result4 = entity.Remove<T4>();
        var result5 = entity.Remove<T5>();
        return result1 && result2 && result3 && result4 && result5;
    }

    #endregion

    #region With

    public static Entity With<T>(this Entity entity)  where T: IComponent, new() {
        entity.Add<T>();
        return entity;
    }
        
    public static Entity With<T>(this Entity entity, T component) where T: IComponent {
        entity.Add(component);
        return entity;
    }

    #endregion
}