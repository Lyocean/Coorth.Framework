namespace Coorth.Serialize; 

public static class SerializeConst {

    public const byte NULL = 0b1111_1111;

    public const byte TRUE = 1;
    public const byte FALSE = 0;

    
    public const byte DATA_SEQUENCE = 2;
    public const byte DATA_EXPLICIT = 3;
    
    public const byte DATA = 1;
    public const byte LIST = 2;
    public const byte DICT = 3;


    #region Types
    
    public const byte TYPE_NULL = 0;
    public const byte TYPE_OBJECT = 1;

    public const byte TYPE_BOOL = 2;
    
    public const byte TYPE_INT8 = 3;
    public const byte TYPE_UINT8 = 4;
    public const byte TYPE_INT16 = 5;
    public const byte TYPE_UINT16 = 6;
    public const byte TYPE_INT32 = 7;
    public const byte TYPE_UINT32 = 8;
    public const byte TYPE_INT64 = 9;
    public const byte TYPE_UINT64 = 10;

    public const byte TYPE_CHAR = 11;
    public const byte TYPE_STRING = 12;

    public const byte TYPE_FLOAT16 = 13;
    public const byte TYPE_FLOAT32 = 14;
    public const byte TYPE_FLOAT64 = 15;
    
    public const byte TYPE_DECIMAL = 16;
    public const byte TYPE_DATETIME = 17;
    public const byte TYPE_TIMESPAN = 18;
    
    public const byte TYPE_VECTOR2 = 30;
    public const byte TYPE_VECTOR3 = 31;
    public const byte TYPE_VECTOR4 = 32;
    public const byte TYPE_QUATERNION = 33;
    public const byte TYPE_MATRIX_32 = 35;
    public const byte TYPE_MATRIX_44 = 36;
    
    public const byte TYPE_TEXT = 200;
    public const byte TYPE_GUID = 201;
    
    #endregion
}