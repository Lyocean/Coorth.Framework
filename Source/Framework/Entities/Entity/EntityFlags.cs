using System.Runtime.CompilerServices;

namespace Coorth.Framework; 

public static class EntityFlags {
    
    public const uint FLAG_MAX_INDEX = 29;
    
    private const uint ENTITY_ACTIVE_FLAG = 0b10u;
    private const uint PARENT_ACTIVE_FLAG = 0b01u;
    public const uint ENTITY_ACTIVE_MASK = 0b11u;
    
    private const byte COMPONENT_ENABLE_FLAG = 0b00000001;
    private const byte COMPONENT_ACTIVE_FLAG = 0b00000010;
    private const byte COMPONENT_ENABLE_MASK = 0b00000011;

    public const uint ENTITY_FLAG_DEFAULT = ENTITY_ACTIVE_MASK;
    public const byte COMPONENT_FLAG_DEFAULT = COMPONENT_ENABLE_MASK;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEntityActive(uint flags) {
        return (flags & ENTITY_ACTIVE_MASK) == ENTITY_ACTIVE_MASK;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetEntityActive(ref uint flags, bool active) {
        if (active) {
            flags |= ENTITY_ACTIVE_FLAG;
        }
        else {
            flags &= ~ENTITY_ACTIVE_FLAG;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetParentActive(ref uint flags, bool active) {
        flags |= PARENT_ACTIVE_FLAG;
        if (active) {
            flags |= PARENT_ACTIVE_FLAG;
        }
        else {
            flags &= ~PARENT_ACTIVE_FLAG;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsComponentEnable(byte flags) {
        return (flags & COMPONENT_ENABLE_MASK) == COMPONENT_ENABLE_MASK;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetComponentEnable(ref byte flags, bool enable) {
        if (enable) {
            flags |= COMPONENT_ENABLE_FLAG;
        }
        else {
            flags = (byte)(flags & (~COMPONENT_ENABLE_FLAG));
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetComponentActive(ref byte flags, bool active) {
        if (active) {
            flags |= COMPONENT_ACTIVE_FLAG;
        }
        else {
            flags = (byte)(flags & (~COMPONENT_ACTIVE_FLAG));
        }
    }

}