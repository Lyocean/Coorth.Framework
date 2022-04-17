using System;

namespace Coorth {
    public partial class AppFrame {
        
        private readonly ModuleRoot root;

        public T Bind<T, TModule>(Func<AppFrame, TModule> func, Action<TModule> init) where TModule : Module<T>, T {
            var module = func(this);
            root.AddModule(module);
            init(module);
            return module;
        }
        
        public T Bind<T, TModule>(TModule module, Action<TModule> init) where TModule : Module<T>, T {
            root.AddModule(module);
            init(module);
            return module;
        }
        
                
        public T Bind<T>(T module) where T : class, IModule {
            root.AddModule(typeof(T), module as Module);
            return module;
        }
        
        public T Add<T, TModule>() where TModule : Module, T, new() {
            var module = new TModule();
            root.AddModule(module);
            return module;
        }
        
        public T Add<T, TModule>(Func<AppFrame, TModule> func) where TModule : Module, T {
            var module = func(this);
            root.AddModule(module);
            return module;
        }
        
        public TModule Add<TModule>() where TModule : Module, new() {
            var module = new TModule();
            root.AddModule(module);
            return module;
        }
        
        public TModule Get<TModule>() where TModule : IModule {
            return root.GetModule<TModule>();
        }
        
        public bool Remove<TModule>() where TModule : Module, new() {
            var module = Get<TModule>();
            module.Dispose();
            return true;
        }
    }
}