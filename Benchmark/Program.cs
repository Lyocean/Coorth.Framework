using BenchmarkDotNet.Running;
using Coorth.Framework;

Console.WriteLine("[Benchmark] Start");

// BenchmarkRunner.Run<HierarchyBenchmark>();
BenchmarkRunner.Run(new Type[] {
    typeof(EntityBenchmark),
    // typeof(HierarchyBenchmark),
});
Console.WriteLine("[Benchmark] Finish");
