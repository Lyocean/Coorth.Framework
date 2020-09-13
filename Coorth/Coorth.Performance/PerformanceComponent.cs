using System;
using System.Diagnostics;
using Coorth.ECS.Test;

namespace Coorth.ECS.Performance {
    public class PerformanceComponent {
        static void WriteResult(Stopwatch s, long temp, long memory, string content) {
            Console.WriteLine($"\t{content}-> time:{s.ElapsedMilliseconds} ms   temp memory:{temp / 1024f / 1024f} Mb   total memory:{memory / 1024f / 1024f} Mb");
        }

        static void TestValComponents(int count) {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[Component] Test Val Component count: {count / 1000} k");
            Console.ResetColor();
            GC.Collect();
            var container = new EcsContainer();
            var entities = new Entity[count];
            for (int i = 0; i < count; i++) {
                var entity = container.CreateEntity();
                entities[i] = entity;
            }
            var s = new Stopwatch();
            s.Start();
            var memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Add<ValComp00>();
                entity.Add<ValComp01>();
                entity.Add<ValComp02>();
                entity.Add<ValComp03>();
                entity.Add<ValComp04>();
                entity.Add<ValComp05>();
                entity.Add<ValComp06>();
                entity.Add<ValComp07>();
                entity.Add<ValComp08>();
                entity.Add<ValComp09>();
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Create Components    ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Has<ValComp00>();
                entity.Has<ValComp01>();
                entity.Has<ValComp02>();
                entity.Has<ValComp03>();
                entity.Has<ValComp04>();
                entity.Has<ValComp15>();
                entity.Has<ValComp16>();
                entity.Has<ValComp17>();
                entity.Has<ValComp18>();
                entity.Has<ValComp19>();
            }
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Contains Components  ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Remove<ValComp00>();
                entity.Remove<ValComp01>();
                entity.Remove<ValComp02>();
                entity.Remove<ValComp03>();
                entity.Remove<ValComp04>();
                entity.Remove<ValComp05>();
                entity.Remove<ValComp06>();
                entity.Remove<ValComp07>();
                entity.Remove<ValComp08>();
                entity.Remove<ValComp09>();
            }
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Delete Components   ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Add<ValComp00>();
                entity.Add<ValComp01>();
                entity.Add<ValComp02>();
                entity.Add<ValComp03>();
                entity.Add<ValComp04>();
                entity.Add<ValComp05>();
                entity.Add<ValComp06>();
                entity.Add<ValComp07>();
                entity.Add<ValComp08>();
                entity.Add<ValComp09>();
            }
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. ReCrate Components ");


            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Destroy();
            }
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Destroy Components ");
        }

        static void TestRefComponents(int count) {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[Component] Test Ref Component count: {count / 1000} k");
            Console.ResetColor();
            GC.Collect();
            var container = new EcsContainer();
            var entities = new Entity[count];
            for (int i = 0; i < count; i++) {
                var entity = container.CreateEntity();
                entities[i] = entity;
            }
            var s = new Stopwatch();
            s.Start();
            var memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Add<RefComp00>();
                entity.Add<RefComp01>();
                entity.Add<RefComp02>();
                entity.Add<RefComp03>();
                entity.Add<RefComp04>();
                entity.Add<RefComp05>();
                entity.Add<RefComp06>();
                entity.Add<RefComp07>();
                entity.Add<RefComp08>();
                entity.Add<RefComp09>();
            }
            s.Stop();
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Create Components    ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Has<RefComp00>();
                entity.Has<RefComp01>();
                entity.Has<RefComp02>();
                entity.Has<RefComp03>();
                entity.Has<RefComp04>();
                entity.Has<RefComp15>();
                entity.Has<RefComp16>();
                entity.Has<RefComp17>();
                entity.Has<RefComp18>();
                entity.Has<RefComp19>();
            }
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Contains Components  ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Remove<RefComp00>();
                entity.Remove<RefComp01>();
                entity.Remove<RefComp02>();
                entity.Remove<RefComp03>();
                entity.Remove<RefComp04>();
                entity.Remove<RefComp05>();
                entity.Remove<RefComp06>();
                entity.Remove<RefComp07>();
                entity.Remove<RefComp08>();
                entity.Remove<RefComp09>();
            }
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Delete Components   ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Add<RefComp00>();
                entity.Add<RefComp01>();
                entity.Add<RefComp02>();
                entity.Add<RefComp03>();
                entity.Add<RefComp04>();
                entity.Add<RefComp05>();
                entity.Add<RefComp06>();
                entity.Add<RefComp07>();
                entity.Add<RefComp08>();
                entity.Add<RefComp09>();
            }
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. ReCrate Components ");

            s.Reset();
            s.Start();
            memory = GC.GetTotalMemory(true);
            for (int i = 0; i < count; i++) {
                var entity = entities[i];
                entity.Destroy();
            }
            WriteResult(s, GC.GetTotalMemory(false) - memory, GC.GetTotalMemory(true) - memory, "1. Destroy Components ");
        }

        public static void TestPerformance() {

            const int MAX_COUNT = 100_000;
            TestValComponents(MAX_COUNT);
            TestRefComponents(MAX_COUNT);
        }
    }
}
