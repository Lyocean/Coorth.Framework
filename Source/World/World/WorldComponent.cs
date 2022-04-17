using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth {
    [Component(Singleton = true), DataContract, Guid("C1F86AB7-787A-40F1-BBAF-D0228F4BBE59")]
    public class WorldComponent : Component {
        
        public World World { get; private set; }

        // public WorldModule Module => World.Module;
        
        // public AppFrame App => World.App;

        // public Infra Infra => App.Infra;

        internal void Setup(World world) {
            this.World = world;
        }
    }
}