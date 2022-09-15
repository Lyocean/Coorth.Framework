namespace Coorth.Serialize;

public interface ISerializable {
    
    public void SerializeReading(SerializeReader reader);
    
    public void SerializeWriting(SerializeWriter writer);
}