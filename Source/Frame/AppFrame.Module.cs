namespace Coorth {
    public partial class AppFrame {
        
        public T Add<T, TModule>() where TModule : ModuleBase, T, new() {
            var module = new TModule();
            root.AddModule(module);
            return module;
        }
        
        public TModule Add<TModule>() where TModule : ModuleBase, new() {
            var module = new TModule();
            root.AddModule(module);
            return module;
        }
        
        public TModule Get<TModule>() where TModule : new() {
            return root.GetModule<TModule>();
        }
        
        public bool Remove<TModule>() where TModule : ModuleBase, new() {
            var module = Get<TModule>();
            if (module != null) {
                module.Dispose();
                return true;
            }
            return false;
        }
    }
}