using System;
using System.Threading;

namespace Coorth; 

public class IdPool {
    
    private readonly int head;

    private readonly ushort id;

    private long second;

    private volatile int current;

    private readonly Func<long> provider;
    
    public IdPool(ushort id, Func<long> provider) {
        this.head = sizeof(short) * 8;
        this.id = id;
        this.provider = provider;
    }
    
    public IdPool(ushort id, int startYear) {
        this.head = sizeof(short) * 8;
        this.id = id;
        this.provider = () => DefaultSecondProvider(startYear);
    }
    
    public IdPool(byte id, int startYear) {
        this.head = sizeof(byte) * 8;
        this.id = id;
        this.provider = () => DefaultSecondProvider(startYear);
    }

    public long Next() {
        if (current >= (1 << head - 1)) {
            var currSecond = provider();
            if (currSecond <= Interlocked.Read(ref second)) {
                SpinWait.SpinUntil(() => provider() > Interlocked.Read(ref second));
            }
            Interlocked.Increment(ref second);
            current = 0;
        }
        var increment = Interlocked.Increment(ref current);
        return ((long) id << (64 - head)) | (second << (32 - head)) | (uint) increment;
    }
    
    private static long DefaultSecondProvider(int startYear) {
        var now = DateTime.UtcNow;
        var start = new DateTime(now.Year - startYear, 1, 1);
        return (long)(now - start).TotalSeconds;
    }
}