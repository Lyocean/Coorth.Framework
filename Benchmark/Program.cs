using BenchmarkDotNet.Running;
using Coorth.Framework;

Console.WriteLine("[Benchmark] Start");

// BenchmarkRunner.Run(new Type[] {
    // typeof(EntityBenchmark),
    // typeof(ComponentBenchmark),
    // typeof(HierarchyBenchmark),
    // typeof(ServiceBenchmark),
// });

var world = new World(new WorldOptions());

var entity = world.CreateEntity();
entity.Add<TransformComponent>();

Console.WriteLine("[Benchmark] Finish");
