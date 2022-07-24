using System.Threading;
using Coorth.Collections;

namespace Coorth; 

public struct IdPool {
    
    private int current;

    private ValueList<int> recycle;
    
    public int Create() {
        if (recycle.Count > 0) {
            return recycle.Pop();
        }
        return Interlocked.Increment(ref current);
    }

    public void Return(int value) {
        recycle.Add(value);
    }
}
