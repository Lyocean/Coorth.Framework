namespace Coorth {
    public interface IWorldActor {
    }

    [ActorProxy, Generated]
    public class WorldProxy : ActorProxy<WorldActor>, IWorldActor {
        public WorldProxy(ActorRef value) : base(value) { }
    }
    
    [Actor]
    public class WorldActor : Actor, IWorldActor, ISetup<WorldModule> {

        private WorldModule Module { get; set; }

        public void OnSetup(WorldModule module) {
            this.Module = module;
        }

        public void CreateSandbox() {
            
        }
        
        public void DestroySandbox() {
            
        }
        
        public void CreateEntity(MessageCreateEntity msg) {
            
        }

        public void DestroyEntity(MessageDestroyEntity msg) {
            
        }
    }
}