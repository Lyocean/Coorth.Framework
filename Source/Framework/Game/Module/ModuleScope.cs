namespace Coorth.Framework; 

public readonly ref struct ModuleScope {
    
    public readonly Module Module;
    
    public ModuleScope(Module value) {
        Module = value;
    }

    public T Add<T>(Module value) where T : IModule => Module.AddModule<T>(value);
    
    public void Dispose() {
    }
}
