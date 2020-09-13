using System;
using Coorth.ECS.Test;

namespace Coorth.ECS.Performance {
    class Program {

        static void Main(string[] args) {
            Console.WriteLine("=================== Coorth ECS Performance Test ===================");
            PerformanceEntity.TestPerformance();
            PerformanceComponent.TestPerformance();
            var contianer = new EcsContainer();
            var entity = contianer.CreateEntity();
            entity.Add<ValComp01>();
            Console.WriteLine(sizeof(bool));
            Console.WriteLine(sizeof(byte));

            Console.WriteLine("=================== Coorth ECS Performance Test ===================");
        }
    }
}
