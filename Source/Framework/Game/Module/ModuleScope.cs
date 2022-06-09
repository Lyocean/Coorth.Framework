namespace Coorth.Framework; 

public readonly ref struct ModuleScope {
    
    private readonly Module module;
    
    public ModuleScope(Module value) {
        module = value;
    }

    public T Add<T>(Module value) where T : IModule => module.AddModule<T>(value);
    
    public void Dispose() {
    }
}