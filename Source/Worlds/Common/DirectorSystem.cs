using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Framework;
using Coorth.Platforms;
using Coorth.Tasks.Ticking;

namespace Coorth.Framework; 

[System, Guid("32B3C700-5454-4C27-83A1-B6B94C6B386D")]
public class DirectorSystem : SystemBase {
    
    protected override void OnAdd() {
        Subscribe<WorldRunTickEvent>().OnEvent(OnRunTick);
    }
    
    private void OnRunTick(WorldRunTickEvent e) {
        
        var platformManager = World.GetService<IPlatformManager>();
        var ticking = new TickingTask(platformManager, e.Setting);
        var thread = new Thread(_ => ticking.RunLoop(World.SyncContext, World.Dispatcher));
        thread.Start();
        
        ticking.OnComplete += () => e.Completion.SetResult(World);
    }
    
}

[Event]
public record WorldRunTickEvent(TickSetting Setting, TaskCompletionSource<World> Completion) : IEvent {
    public readonly TickSetting Setting = Setting;
    public readonly TaskCompletionSource<World> Completion = Completion;
}
