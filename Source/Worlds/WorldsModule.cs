using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Coorth.Framework;
using Coorth.Logs;
using Coorth.Worlds;

namespace Coorth.Worlds; 

public interface IWorldsModule : IModule {
    World Main { get; }
}

[Module, Guid("26D3438C-F2CC-4DC3-AAAF-D5EC74E97E9D")]
public class WorldsModule : Module, IWorldsModule {

    private readonly World main;
    public World Main => main;
    

    public WorldsModule(AppFrame app, ActorLocalDomain domain, string name) {
        var options = new WorldOptions(name, app, this, app.Services);

        main = World.Create(domain, options);
        main.SetActive(false);
        main.ManageBy(ref Managed);
    }

    protected override void OnActive() {
        Main.SetActive(true);
    }
        
    protected override void OnDeActive() {
        Main.SetActive(false);
    }
}