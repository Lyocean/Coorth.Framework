using System;

namespace Coorth {
    public abstract class TextSerializeWriter : SerializeWriter {
        public override void WriteDateTime(DateTime value) => WriteString(value.ToLongTimeString());

        public override void WriteTimeSpan(TimeSpan value) => WriteString(value.ToString());

        public override void WriteGuid(Guid value) => WriteString(value.ToString());

        public override void WriteType(Type value) => WriteString(value.FullName ?? string.Empty);

        public override void WriteEnum<T>(T value) => WriteString(value.ToString() ?? string.Empty);

        protected string GetTypeName(Type type) => type.FullName ?? string.Empty;
    }
}