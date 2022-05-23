using System;

namespace Coorth.Serialize;

public abstract class TextSerializeWriter : BaseSerializeWriter {
    protected override void WriteDateTime(DateTime value) {
        WriteString(value.ToLongTimeString());
    }

    protected override void WriteTimeSpan(TimeSpan value) {
        WriteString(value.ToString());
    }

    protected override void WriteGuid(Guid value) {
        WriteString(value.ToString());
    }

    protected override void WriteType(Type value) {
        var guid = TypeBinding.GetGuid(value);
        WriteString(guid != Guid.Empty ? $"Guid:{guid}" : $"Name:{value.FullName}");
    }

    protected override void WriteEnum<T>(T value) {
        WriteString(value?.ToString() ?? string.Empty);
    }

    protected string GetTypeName(Type type) {
        return type.FullName ?? string.Empty;
    }
}