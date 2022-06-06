using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Coorth.Framework;
using Coorth.Logs;
using Coorth.Tasks.Ticking;

namespace Coorth.Worlds; 

[Serializable, DataContract]
[Component(Singleton = true), Guid("5A03F9B7-5CB3-4C20-BBFD-5A14AA09F13E")]
public class DirectorComponent : Component {
    
    public volatile bool IsRunning;
        
    public readonly TaskCompletionSource<bool> CompletionSource = new();

    public TimeSpan OffsetTime = TimeSpan.Zero;

    public bool IsReflectionEnable;

    public bool IsDebug;

    public readonly ILogger Logger;
    
    public World World { get; private set; }

    public WorldsModule Module => World.Module;

    public AppFrame App => World.App;

    public DirectorComponent(World world, ILogger logger) {
        World = world;
        Logger = logger;
    }
    
    public void Setup(World world) {
        World = world;
    } 

    public void SetCurrentTime(DateTime time) {
        OffsetTime = time - DateTime.UtcNow;
    }
        
    public DateTime GetCurrentTime() {
        return DateTime.UtcNow + OffsetTime;
    }
}