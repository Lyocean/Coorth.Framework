namespace Coorth {
    using Logs;
    
    [Component, StoreContract("49672219-0612-4E7D-A0C5-EAC8CF6F554C")]
    public class DebugComponent : Component {
        
        public bool IsReflectionEnable = false;

        private ILogger logger;

        public ILogger Logger {
            get => logger ??= Sandbox.Services.GetService<LogManager>().Root;
            set => logger = value;
        }
    }
}