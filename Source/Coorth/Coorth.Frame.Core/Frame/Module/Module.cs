using System;
using System.Collections.Generic;

namespace Coorth {
    public class Module : ServiceBase {
        
        private Module parent;
        public Module Parent => parent;
        
        private readonly Dictionary<Type, Module> children = new Dictionary<Type, Module>();
        public IReadOnlyDictionary<Type, Module> Children => children;

        public void Add<TModule>(TModule module = default) where TModule : Module, new() {
            var type = module != null ? module.GetType() : typeof(TModule);
            module = module ?? Activator.CreateInstance<TModule>();
            children.Add(type, module);
            module.parent = this;
            module.ServiceAdd(Services);
        }

        public TModule Get<TModule>() {
            var module = Get(typeof(TModule));
            if (module is TModule result) {
                return result;
            }
            return default;
        }

        public Module Get(Type type) {
            return children.TryGetValue(type, out var module) ? module : default;
        }
        
        public bool Remove(Type type) {
            if (children.TryGetValue(type, out var module)) {
                module.ServiceRemove();
                return true;
            }
            return false;
        }
    }

    public class RootModule : Module {

    }
}