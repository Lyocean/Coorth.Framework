using System;

namespace Coorth {
    public static class App {

        public static Infra Infra => Infra.Root;

        public static World World => World.Root;

        public static Modul Modul => Modul.Root;

        public static void Boot() { }

        public static void Loop() { }

        public static void Down() { }

    }
}
