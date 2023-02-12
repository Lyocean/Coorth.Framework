namespace Coorth.Serialize;

public interface ISerializable {
    
    void SerializeReading(ISerializeReader reader);

    void SerializeWriting(ISerializeWriter writer);

}

public interface ISerializable<T> : ISerializable where T: ISerializable<T> {
    
#if NET7_0_OR_GREATER
    
    static virtual void SerializeReading(in ISerializeReader reader, scoped ref T? value) => value?.SerializeReading(reader);

    static virtual void SerializeWriting(in ISerializeWriter writer, scoped in T value) => value?.SerializeWriting(writer);

#endif
}