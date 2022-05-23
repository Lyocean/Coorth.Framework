using System;
using System.Threading;

namespace Coorth.Framework;

public static class Infra {
        
    private static readonly ServiceLocator services = new();

    public static ServiceLocator Services => services;

    private struct Impl<T> where T: class { public static T? Inst; }

    public static IServiceBinding Bind(Type type) => services.Bind(type);

    public static IServiceBinding Bind(Type type, Type implType) => services.Bind(type);

    public static ServiceBinding<T> Bind<T>() where T : class, new()  => services.Bind<T>();

    public static ServiceBinding<T> Bind<T>(Func<ServiceLocator, T> provider) where T : class  => services.Bind(provider);

    public static ServiceBinding<T> Bind<T>(T instance) where T : class  => services.Bind(instance);

    public static ServiceBinding<TImpl> Bind<T, TImpl>() where TImpl : class, T, new() => services.Bind<T, TImpl>();

    public static T Offer<T>() where T : class, new() {
        if (Impl<T>.Inst != null) {
            return Impl<T>.Inst;
        }
        var binding = services.GetBinding<T>().Binding;
        var instance = binding.Singleton<T>();
        Interlocked.Exchange(ref Impl<T>.Inst, instance);
        return instance;          
    }

    public static T Get<T>() where T : class {
        if (Impl<T>.Inst != null) {
            return Impl<T>.Inst;
        }
        var inst = services.Singleton<T>();
        Interlocked.Exchange(ref Impl<T>.Inst, inst);
        if (Impl<T>.Inst == null) {
            throw new NotBindException();
        }
        return Impl<T>.Inst;
    }
}
