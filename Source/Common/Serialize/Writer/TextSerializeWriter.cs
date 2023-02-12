using System;
using System.Runtime.CompilerServices;

namespace Coorth.Serialize; 

public abstract class TextSerializeWriter : SerializeWriter {
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void WriteDateTime(DateTime value) => WriteString(value.ToLongTimeString());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void WriteTimeSpan(TimeSpan value) => WriteString(value.ToString());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void WriteGuid(Guid value) => WriteString(value.ToString());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override void WriteType(Type? value) {
        if (value == null) {
            WriteString("Null");
            return;
        }
        var guid = TypeBinding.GetGuid(value);
        WriteString(guid != Guid.Empty ? $"Guid:{guid}" : $"Name:{value?.FullName}");
    }
    
    public override void WriteEnum<T>(T value) {
        WriteString(value.ToString());
    }

    public override void WriteEnum(Type type, object value) {
        WriteString(value.ToString());
    }
}