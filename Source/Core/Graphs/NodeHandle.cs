using System;

namespace Coorth {
    public readonly struct NodeHandle {
        public readonly Type Type;
        public readonly Guid Id;

        public NodeHandle(Type type, Guid id) {
            this.Type = type;
            this.Id = id;
        }
        
        public NodeHandle<T> Cast<T>() {
            if (!typeof(T).IsAssignableFrom(Type)) {
                throw new InvalidCastException();
            }
            return new NodeHandle<T>(Id);
        }
    }

    public readonly struct NodeHandle<TVertex> {
        public readonly Guid Id;

        public NodeHandle(Guid id) {
            this.Id = id;
        }

        public NodeHandle Cast() {
            return new NodeHandle(typeof(TVertex), Id);
        }

        public static implicit operator NodeHandle(NodeHandle<TVertex> handle) {
            return handle.Cast();
        }

        public static implicit operator NodeHandle<TVertex>(NodeHandle handle) {
            return handle.Cast<TVertex>();
        }
    }
}