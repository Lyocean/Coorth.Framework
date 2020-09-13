using System;
using System.Diagnostics;

namespace Coorth.ECS.Performance {
    internal static class PerformanceEntity {

        static void WriteResult(Stopwatch s, long temp, long memory, string content) {
            Console.WriteLine($"\t{content}-> time:{s.ElapsedMilliseconds} ms   temp memory:{temp/1024f/1024f} Mb   total memory:{memory / 1024f / 1024f} Mb");
        }

        static void TestContext(int count) {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[Entity] Test EntityContext count: {count/1000} k");
            Console.ResetColor();
            GC.Collect();
            var container = new EcsContainer();
            var contexts = new EntityContext[count];
            var s = new Stopwatch();
            s.Start();
            var memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                contexts[i] = container.CreateContext();
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Create Context    ");
            
            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                container.HasEntity(contexts[i].Id);
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "2. Has Context       ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                contexts[i].Destroy();
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "3. Destroy Context   ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                contexts[i] = container.CreateContext();
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "4. ReCreate Context  ");
        }

        static void TestEntity(int count) {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[Entity] Test Entity count: {count / 1000} k");
            Console.ResetColor();
            var container = new EcsContainer();
            var entities = new Entity[count];
            GC.Collect();
            var s = new Stopwatch();
            s.Start();
            var memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                entities[i] = container.CreateEntity();
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Create Entity     ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                container.HasEntity(entities[i].Id);
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "2. Has Entity        ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                entities[i].Destroy();
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "3. Destroy Entity    ");

            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                entities[i] = container.CreateEntity();
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "4. ReCreate Entity   ");
        }

        public static void TestPerformance() {
            const int MAX_COUNT = 100_000;
            TestContext(MAX_COUNT);
            TestEntity(MAX_COUNT);
        }
    }
}
