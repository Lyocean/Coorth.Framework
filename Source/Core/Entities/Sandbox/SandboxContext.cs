using System.Threading;

namespace Coorth {
    public class SandboxContext {

        private Thread MainThread { get; set; }

        public int MainThreadId { get; private set; }
        
        public void Startup(Thread mainThread) {
            MainThread = mainThread;
            MainThreadId = MainThread.ManagedThreadId;
        }
    }
}