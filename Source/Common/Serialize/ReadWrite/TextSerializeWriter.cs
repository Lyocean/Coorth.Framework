using System;

namespace Coorth {
    public abstract class TextSerializeWriter : SerializeWriter {
        public override void WriteDateTime(DateTime value) {
            // LogUtil.Debug($"WriteDateTime:{value}");

            WriteString(value.ToLongTimeString());
        }

        public override void WriteTimeSpan(TimeSpan value) {
            // LogUtil.Debug($"WriteTimeSpan:{value}");

            WriteString(value.ToString());
        }

        public override void WriteGuid(Guid value) {
            // LogUtil.Debug($"WriteGuid:{value}");

            WriteString(value.ToString());
        }

        public override void WriteType(Type value) {
            // LogUtil.Debug($"WriteType:{value}");

            var guid = TypeBinding.GetGuid(value);
            if (guid != Guid.Empty) {
                WriteString($"Guid:{guid}");
                // WriteString($"{value.Name}|{guid}");
            }
            else {
                WriteString($"Name:{value.FullName}");
            }
        }

        public override void WriteEnum<T>(T value) {
            // LogUtil.Debug($"WriteEnum:{value}");

            WriteString(value.ToString());
        }

        protected string GetTypeName(Type type) {
            return type.FullName;
        }
        
    }
}