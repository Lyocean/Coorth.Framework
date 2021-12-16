namespace Coorth {
    public abstract partial class AppFramework {

        private ModuleRoot root;

        private LocalDomain domain;
        
        public void BindModule<TInterface, TImplement>() {
            
        }
        
        public TModule CreateModule<TModule>() where TModule : class, IActor, new() {
            var module = new TModule();

            AddModule(module);
            return default;
        }
        
        public TModule AddModule<TModule>(TModule module) where TModule : class, IActor, new() {
            var actorRef = domain.CreateActor<TModule>(module, typeof(TModule).Name);
            return default;
        }
        
        public TModule GetModule<TModule>() {

            return default;
        }
        
        public bool RemoveModule<TModule>() {

            return default;
        }
    }
}