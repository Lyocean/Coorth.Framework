using System;
using System.Threading.Tasks;
using Coorth.Logs;

namespace Coorth.Common {
    [Component(Singleton = true), StoreContract("5A03F9B7-5CB3-4C20-BBFD-5A14AA09F13E")]
    public class SandboxComponent : Component {
        
        public volatile bool IsRunning;
        
        public TimeTicker Ticker;
        
        public readonly TaskCompletionSource<bool> CompletionSource = new TaskCompletionSource<bool>();

        public TimeSpan OffsetTime = TimeSpan.Zero;

        public bool IsReflectionEnable = false;

        public bool IsDebug;
        
        private ILogger logger;

        public ILogger Logger {
            get => logger ??= Sandbox.Services.GetService<LogManager>().Root;
            set => logger = value;
        }
        
        public void SetCurrentTime(DateTime time) {
            this.OffsetTime = time - DateTime.UtcNow;
        }
        
        public DateTime GetCurrentTime() {
            return DateTime.UtcNow + OffsetTime;
        }
    }
}