using System;
using Coorth.Serialize;

namespace Coorth.Framework; 

[Serializable, StoreContract]
public readonly record struct ActorPath {
    
    public readonly string FullPath;
    
    private readonly int index;
    
    public ReadOnlySpan<char> Parent => FullPath.AsSpan(0, index);
    
    public readonly string Name;
    
    public ActorPath(string parent, string name) {
        FullPath = string.IsNullOrEmpty(parent) ? name : parent + "/" + name;
        index = 0;
        Name = name;
    }

    public ActorPath(string fullPath) {
        FullPath = fullPath;
        if (fullPath.Contains('/')) {
            index = fullPath.LastIndexOf("/", StringComparison.Ordinal);
            Name = fullPath[(index + 1)..];
        }
        else {
            index = 0;
            Name = fullPath;
        }
    }
    
    public static explicit operator ActorPath(string fullPath) => new(fullPath);

    public static explicit operator string(ActorPath path) => path.FullPath;

    [Serializer(typeof(Serializer))]
    public class Serializer : Serializer<ActorPath> {
        public override void Write(SerializeWriter writer, in ActorPath value) {
            writer.WriteValue(value.FullPath);
        }

        public override ActorPath Read(SerializeReader reader, ActorPath value) {
            var path = reader.ReadValue<string>();
            return new ActorPath(path ?? string.Empty);
        }
    }
}