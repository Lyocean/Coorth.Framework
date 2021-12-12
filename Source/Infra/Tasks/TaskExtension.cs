using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    public static class TaskExtension {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Forget(this Task task) {
            if (task == null) {
                throw new ArgumentNullException();
            }
        }
    }
}