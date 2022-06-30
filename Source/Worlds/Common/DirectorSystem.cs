using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Coorth.Framework;
using Coorth.Platforms;
using Coorth.Tasks.Ticking;

namespace Coorth.Worlds; 

[System, Guid("32B3C700-5454-4C27-83A1-B6B94C6B386D")]
public class DirectorSystem : SystemBase {
    
    protected override void OnAdd() {
        Sandbox.BindComponent<DirectorComponent>();
        Subscribe<EventSandboxRunTick>().OnEvent(OnRunTick);
    }
    
    private void OnRunTick(EventSandboxRunTick e) {
        
        var platformManager = Sandbox.GetService<IPlatformManager>();
        var ticking = new TickingTask(platformManager, e.Setting);
        var thread = new Thread(_ => ticking.RunLoop(Sandbox.Schedule, Sandbox.Dispatcher));
        thread.Start();
        
        ticking.OnComplete += () => e.Completion.SetResult(Sandbox);
    }
    
}

[Event]
public record EventSandboxRunTick(TickSetting Setting, TaskCompletionSource<Sandbox> Completion) : IEvent {
    public readonly TickSetting Setting = Setting;
    public readonly TaskCompletionSource<Sandbox> Completion = Completion;
}
