namespace Coorth {
    public readonly struct ComponentPtr<T> where T : IComponent {
        
        private readonly ComponentGroup<T> group;
        
        private readonly int index;

        public Sandbox Sandbox => group.Sandbox;
        
        public ref T Get() => ref group.Get(index);

        public Entity Entity => Sandbox.GetEntity(group.GetEntityIndex(index));

        internal ComponentPtr(ComponentGroup<T> group, int index) {
            this.group = group;
            this.index = index;
        }
    }
}