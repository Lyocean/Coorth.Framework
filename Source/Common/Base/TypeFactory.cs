using System;

namespace Coorth;

public static class TypeFactory {

    public static void Bind<T>(Func<T> provider) {
        Binding<T>.Provider = provider;
    }

    public static T Create<T>() where T: class {
        return Binding<T>.Provider?.Invoke() 
               ?? Activator.CreateInstance<T>() 
               ?? throw new NotImplementedException();
    }
    
    
    private readonly record struct Binding<T> {
        public static Func<T>? Provider;
    }

}