using BenchmarkDotNet.Running;

using Coorth.Framework;

namespace Coorth.Framework; 

public static class Program {
    private static void Main(string[] args) {
        BenchmarkRunner.Run<ServiceBenchmark>();
        BenchmarkRunner.Run<EntityBenchmark>();
    }
}