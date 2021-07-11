using System;

namespace Coorth {
    public readonly struct ComponentBinding<T> where T: IComponent {

        private readonly Sandbox sandbox;
        
        private readonly ComponentGroup<T> group;
        
        internal ComponentBinding(Sandbox sandbox, ComponentGroup<T> group) {
            this.sandbox = sandbox;
            this.group = group;
        }

        public ComponentBinding<T> AddDependency<TP1>() where TP1: IComponent{
            var componentGroup1 = sandbox.GetComponentGroup<TP1>();
            group.AddDependency(componentGroup1);
            return this;
        }

        public ComponentBinding<T> AddDependency<TP1, TP2>()where TP1: IComponent where TP2: IComponent {
            var componentGroup1 = sandbox.GetComponentGroup<TP1>();
            var componentGroup2 = sandbox.GetComponentGroup<TP2>();
            group.AddDependency(componentGroup1);
            group.AddDependency(componentGroup2);
            return this;
        }
        
        public ComponentBinding<T> AddDependency<TP1, TP2, TP3>() where TP1: IComponent where TP2: IComponent where TP3: IComponent{
            var componentGroup1 = sandbox.GetComponentGroup<TP1>();
            var componentGroup2 = sandbox.GetComponentGroup<TP2>();
            var componentGroup3 = sandbox.GetComponentGroup<TP3>();
            group.AddDependency(componentGroup1);
            group.AddDependency(componentGroup2);
            group.AddDependency(componentGroup3);
            return this;
        }

        public bool HasDependency<TComp2>() where TComp2 : IComponent {
            return group.HasDependency(typeof(TComp2));
        }

        public ComponentBinding<T> WithCreator(ComponentCreator<T> func) {
            group.Creator = func;
            return this;
        }

        public ComponentBinding<T> WithCloner(ComponentCloner<T> func) {
            group.Cloner = func;
            return this;
        }
        
        public ComponentBinding<T> WithRecycler(ComponentRecycler<T> func) {
            group.Recycler = func;
            return this;
        }
    }
}