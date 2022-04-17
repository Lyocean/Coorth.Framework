using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Coorth {
    public interface IWorldModule : IModule {
        World World { get; }
    }
    
    [Module, DataContract, Guid("26D3438C-F2CC-4DC3-AAAF-D5EC74E97E9D")]
    public class WorldModule : Module<IWorldModule>, IWorldModule {

        private readonly World world;
        public World World => world;

        private ActorRef actor;

        public WorldModule(AppFrame app, WorldConfig config) {
            this.world = new World(app, config, this);
            this.world.SetActive(false);
            this.world.ManageBy(ref Managed);
        }
        
        protected override void OnAdd() {
            LogUtil.Info(nameof(WorldModule), nameof(OnAdd));
            this.actor = Domain.CreateActor<WorldActor, WorldModule>(nameof(WorldActor), this);
        }
        
        protected override void OnActive() {
            LogUtil.Info(nameof(WorldModule), nameof(OnActive));
            this.World.SetActive(true);
        }
        
        protected override void OnDeActive() {
            LogUtil.Info(nameof(WorldModule), nameof(OnDeActive));
            this.World.SetActive(false);
        }
    }
}