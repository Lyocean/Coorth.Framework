using BenchmarkDotNet.Running;
using Coorth.Framework;

//public class Program {

//    public static void Main(string[] args) {
//        BenchmarkRunner.Run<ServiceBenchmark>();
//    }
//}

// var service = new ServiceBenchmark();
// service.Setup();
// service.IterationSetup();
// service.BindService();
// service.NewService();
// service.FirstGetService();
// service.RepeatGetService();

//BenchmarkRunner.Run<ServiceBenchmark>();
BenchmarkRunner.Run<EntityBenchmark>();
