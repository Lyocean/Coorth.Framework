using System;

namespace Coorth {
    public abstract class TextSerializeReader : SerializeReader {
        public override DateTime ReadDateTime() {
            var text = ReadString();
            return DateTime.Parse(text);
        }

        public override TimeSpan ReadTimeSpan() {
            var text = ReadString();
            return TimeSpan.Parse(text);
        }

        public override Guid ReadGuid() {
            var text = ReadString();
            return Guid.Parse(text);
        }

        public override Type ReadType() {
            string text = ReadString();
            int index = text.IndexOf(":", StringComparison.Ordinal);
            if (text.StartsWith("Guid:")) {
                return TypeBinding.GetType(text.Substring(index+1));
            }
            if(text.StartsWith("Name:")) {
                return Type.GetType(text.Substring(index+1));
            }
            throw new InvalidCastException($"[Serialize]: Unexpected type format: {text}.");
        }

        public override T ReadEnum<T>() {
            var text = ReadString();
            // UnityEngine.Debug.Log($"{typeof(T)} --> {text}");
            return (T)Enum.Parse(typeof(T), text);
        }
    }
}