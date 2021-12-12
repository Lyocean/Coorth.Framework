using System.Collections.Generic;

namespace Coorth.Serializes {
    public class ArraySerializer<TItem> : CollectionSerialier<TItem[]> {

        public override TItem[] Read(SerializeReader reader, TItem[] value) {
            reader.ReadList(ref value);
            return value;
        }

        public override void Write(SerializeWriter writer, in TItem[] value) {
            writer.WriteList(value);
        }
    }

    public class ListSerializer<TItem> : CollectionSerialier<IList<TItem>> {

        public override IList<TItem> Read(SerializeReader reader, IList<TItem> value) {
            List<TItem> list = value as List<TItem>;
            reader.ReadList(ref list);
            return list;
        }

        public override void Write(SerializeWriter writer, in IList<TItem> value) {
            writer.WriteList(in value);
        }
    }

    public class DictSerializer<TKey, TValue> : CollectionSerialier<IDictionary<TKey, TValue>> {
        public override IDictionary<TKey, TValue> Read(SerializeReader reader, IDictionary<TKey, TValue> value) {
            Dictionary<TKey, TValue> dict = value as Dictionary<TKey, TValue>;
            reader.ReadDict(ref dict);
            return dict;
        }

        public override void Write(SerializeWriter writer, in IDictionary<TKey, TValue> value) {
            writer.WriteDict(value);
        }
    }
}
