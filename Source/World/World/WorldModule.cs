namespace Coorth {
    public interface IWorldModule {
        World World { get; }
    }
    
    [Module, StoreContract("26D3438C-F2CC-4DC3-AAAF-D5EC74E97E9D")]
    public class WorldModule : ModuleBase, IWorldModule {

        private World world;
        public World World => world;

        private ActorRef actor;

        protected override void OnAdd() {
            LogUtil.Info(nameof(WorldModule), nameof(OnAdd));
            this.world = new World(App, new WorldConfig(), this);
            this.world.ManageBy(ref Managed);
            this.world.SetActive(false);
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