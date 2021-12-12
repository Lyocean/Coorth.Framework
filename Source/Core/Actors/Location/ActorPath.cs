using System;
using System.Runtime.Serialization;

namespace Coorth {
    [Serializable, DataContract]
    public readonly struct ActorPath : IEquatable<ActorPath> {
        public readonly string Address;
        public readonly string Parent;
        public readonly string Name;
        public readonly string FullPath;
        
        public bool IsRoot => Parent == null && Name == "/";

        public ActorPath(string address, string parent, string name) {
            this.Address = address;
            this.Parent = parent;
            this.Name = name;
            this.FullPath = parent == null ? name : parent + "/" + name;
        }
        
        public ActorPath(string parent, string name) {
            this.Address = null;
            this.Parent = parent;
            this.Name = name;
            this.FullPath = parent == null ? name : parent + "/" + name;
        }

        public ActorPath(string fullPath) {
            if (fullPath.Contains(":")) {
                if (!fullPath.Contains("/")) {
                    throw new ArgumentException($"ActorPath error: {fullPath}");
                }
                this.Address = fullPath.Substring(0, fullPath.IndexOf("/", StringComparison.Ordinal));
            }
            else {
                this.Address = null;
            }
            this.FullPath = fullPath;
            if (!fullPath.Contains("/")) {
                Parent = null;
                Name = fullPath;
            }
            else {
                var index = fullPath.LastIndexOf("/", StringComparison.Ordinal);
                Parent = fullPath.Substring(0, index);
                Name = fullPath.Substring(index + 1);
            }
        }
        
        public static explicit operator ActorPath(string fullPath) {
            return new ActorPath(fullPath);
        }
        
        public static explicit operator ActorPath((string,string) tuple) {
            return new ActorPath(tuple.Item1, tuple.Item2);
        }
        
        public static explicit operator string(ActorPath path) {
            return path.FullPath;
        }
        
        public static explicit operator (string,string)(ActorPath path) {
            return (path.Parent, path.Name);
        }
        
        public override bool Equals(object obj) {
            return obj != null && obj is ActorPath path  && this.Equals(path);
        }

        public static bool operator ==(ActorPath a, ActorPath b) {
            return a.Equals(b);
        }

        public static bool operator !=(ActorPath a, ActorPath b) {
            return !a.Equals(b);
        }
        
        public override int GetHashCode() {
            return FullPath != null ? FullPath.GetHashCode() : 0;
        }

        public override string ToString() {
            return $"[ActorPath]: {FullPath}";
        }

        public bool Equals(ActorPath other) {
            return Parent == other.Parent && Name == other.Name && FullPath == other.FullPath;
        }
    }
}