using System.Collections.Generic;

namespace Coorth {
    public class World : Domain<World> {

        public static Sandbox Active => Get<Sandbox>();

        public static Sandbox Offer(string name) {
            return Active;
        }
    }
}
