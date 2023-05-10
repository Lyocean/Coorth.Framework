using BenchmarkDotNet.Running;
using Coorth.Framework;

Console.WriteLine("[Benchmark] Start");

BenchmarkRunner.Run(new Type[] {
    // typeof(EntityBenchmark),
    typeof(ComponentBenchmark),
    // typeof(HierarchyBenchmark),
    // typeof(ServiceBenchmark),
});

Console.WriteLine("[Benchmark] Finish");
