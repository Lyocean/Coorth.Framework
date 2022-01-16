using System;

namespace Coorth.Serializes {
    [Flags]
    public enum StoreTypeFlags : byte {
        ByName = 0,
        ByGuid = 1,
    }
}