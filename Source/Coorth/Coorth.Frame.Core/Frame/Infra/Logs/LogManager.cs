using System;

namespace Coorth {
    using Coorth.Logs;
    public class LogManager : ManagerBase {
        
        public ILogger Root { get; private set; }

        public void Setup<TLogger>() where TLogger : class, ILogger, new() {
            Root = Activator.CreateInstance<TLogger>();
            Root.Setup(this, Services);
            LogUtil.Bind(Root);
        }
        
        public void Setup<TLogger>(TLogger logger) where TLogger : class, ILogger {
            Root = logger ?? Activator.CreateInstance<TLogger>();
            Root.Setup(this, Services);
            LogUtil.Bind(Root);
        }

        protected override void Dispose(bool dispose) {
            Root.Dispose();
            Root = null;
        }
    }
}