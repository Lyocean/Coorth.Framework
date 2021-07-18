using System;

namespace Coorth {
    public class ServiceContainer : ServiceFactory {

        public readonly EventDispatcher Dispatcher = new EventDispatcher();
        
        protected override void OnCreateObject(Type type, object instance) {
            if (instance is ServiceBase system) {
                system.SystemAdd(this);
            }
            base.OnCreateObject(type, instance);
        }

        protected override void OnDestroyObject(Type type, object instance) {
            if (instance is ServiceBase system) {
                system.SystemRemove();
            }
            base.OnDestroyObject(type, instance);
        }
    }
}