using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Common {
    [Component(Singleton = true), DataContract, Guid("5A03F9B7-5CB3-4C20-BBFD-5A14AA09F13E")]
    public class SandboxComponent : Component {
        
        public volatile bool IsRunning;
        
        public TimeTicker Ticker;
        
        public readonly TaskCompletionSource<bool> CompletionSource = new TaskCompletionSource<bool>();

        public TimeSpan OffsetTime = TimeSpan.Zero;

        public bool IsReflectionEnable = false;

        public bool IsDebug;
        
        private ILogger? logger;
        public ILogger Logger { get => logger ??= Sandbox.GetService<ILogManager>().Create(Sandbox.Name); set => logger = value; }
        
        public World World { get; private set; }

        public WorldModule Module => World.Module;

        public AppFrame App => World.App;

        public void Setup(World world) {
            this.World = world;
        } 

        public void SetCurrentTime(DateTime time) {
            this.OffsetTime = time - DateTime.UtcNow;
        }
        
        public DateTime GetCurrentTime() {
            return DateTime.UtcNow + OffsetTime;
        }
    }
}