using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coorth {
    public interface IWorldModule {
        
    }
    
    [Module, StoreContract("26D3438C-F2CC-4DC3-AAAF-D5EC74E97E9D")]
    public class WorldModule : ModuleBase, IWorldModule {
        
        private World main;

        private readonly Dictionary<ActorId, World> worlds = new Dictionary<ActorId, World>();
        public IReadOnlyDictionary<ActorId, World> Worlds => worlds;

        public Task CreateWorld(string name, WorldConfig config = null) {
            var world = new World(default, config);
            if (main == null) {
                main = world;
            }
            return Task.CompletedTask;
        }
        
        public Task GetWorld(ActorId id) {
            
            return Task.CompletedTask;
        }

        public Task DestroyWorld(ActorId id) {
            
            return Task.CompletedTask;
        }
    }
}