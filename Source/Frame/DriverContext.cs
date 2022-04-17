using System.Threading;

namespace Coorth {
    public class DriverContext {
        
        private Thread mainThread;

        /// <summary> App主线程Id </summary>
        public int MainThreadId => mainThread.ManagedThreadId;

        public TimeTicker Ticker;
        
        
    }
}