using System;
using System.IO;

namespace Coorth {

    public interface ISerializer {
        void WriteValue(Type type, object value, BinaryWriter writer);
        object ReadValue(Span<byte> span);
        
    }
    
    public abstract class Serializer {
     
    }


    public interface ISerializerRegion {
        void EndScope();
        void EndList();
        void EndDict();
    }
    
    public readonly ref struct SerializeScope {

        private readonly ISerializerRegion serializer;

        public SerializeScope(ISerializerRegion value) {
            this.serializer = value;
        }

        public void Dispose() {
            serializer.EndScope();
        }
    }
    
    public readonly ref struct SerializeList {
        private readonly ISerializerRegion serializer;

        public SerializeList(ISerializerRegion value) {
            this.serializer = value;
        }

        public void Dispose() {
            serializer.EndList();
        }
    }

    public readonly ref struct SerializeDict {
        private readonly ISerializerRegion serializer;

        public SerializeDict(ISerializerRegion value) {
            this.serializer = value;
        }

        public void Dispose() {
            serializer.EndDict();
        }
    }
}
