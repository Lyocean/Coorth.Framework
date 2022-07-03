using System;
using System.Runtime.InteropServices;
using Coorth.Framework;

namespace Coorth.Worlds; 

[Serializable, DataContract]
[Component(Singleton = true), Guid("5A03F9B7-5CB3-4C20-BBFD-5A14AA09F13E")]
public class DirectorComponent : Component {

    public TimeSpan OffsetTime = TimeSpan.Zero;

    public bool IsDebug;

    public void SetCurrentTime(DateTime time) {
        OffsetTime = time - DateTime.UtcNow;
    }
        
    public DateTime GetCurrentTime() {
        return DateTime.UtcNow + OffsetTime;
    }
}