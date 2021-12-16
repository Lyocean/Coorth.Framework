using System;
using Coorth.Logs;

namespace Coorth {
    [Manager]
    public class LogManager : Management, ILogManager {
        
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

        protected override void OnDispose(bool dispose) {
            Root.Dispose();
            Root = null;
        }
    }
}