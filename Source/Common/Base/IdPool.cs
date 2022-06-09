using System;
using System.Threading;

namespace Coorth; 

public class IdPool {
    
    private readonly short poolId;

    private readonly Func<long> secondProvider;

    private long lastSecond;
    
    private volatile int currentId;
    
    public IdPool(short poolId, Func<long> secondProvider) {
        this.poolId = poolId;
        this.secondProvider = secondProvider;
    }
    
    public IdPool(short poolId, int startYear) {
        this.poolId = poolId;
        this.secondProvider = () => DefaultSecondProvider(startYear);
    }

    public long Next() {
        if (currentId >= ushort.MaxValue) {
            var currSecond = secondProvider();
            if (currSecond <= Interlocked.Read(ref lastSecond)) {
                SpinWait.SpinUntil(() => secondProvider() > Interlocked.Read(ref lastSecond));
            }
            Interlocked.Increment(ref lastSecond);
            currentId = 0;
        }
        var increment_part = Interlocked.Increment(ref currentId);
        var id = ((long) poolId << 48)| (lastSecond << 32) | (uint) increment_part;
        return id;
    }
    
    private static long DefaultSecondProvider(int startYear) {
        var now = DateTime.UtcNow;
        var start = new DateTime(now.Year - startYear, 1, 1);
        return (long)(now - start).TotalSeconds;
    }
    
}