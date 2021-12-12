using System;
using System.Threading.Tasks;

namespace Coorth {
    [Component, StoreContract("5B494D7C-597D-4D08-9992-28FEC170DB78")]
    public class ScriptComponent : Component, IDisposable {

        public bool IsDisposed { get; private set; }

        public bool IsEnable { get; private set;} 

        protected ScriptSystem ScriptSystem => Sandbox.GetSystem<ScriptSystem>();

        protected EventDispatcher Dispatcher => ScriptSystem.GetDispatcher();

        public Task<T> DelayFrame<T>(int frameCount) where T : ITickEvent => Dispatcher.DelayFrame<T>(frameCount);
        
        public Task DelayFrame(int frameCount) => Dispatcher.DelayFrame(frameCount);

        public Task<T> DelayTime<T>(TimeSpan duration) where T : ITickEvent => Dispatcher.DelayTime<T>(duration);
        
        public Task DelayTime(TimeSpan duration) => Dispatcher.DelayTime(duration);

        public Task<T> UntilCondition<T>(Func<T, bool> condition) where T : ITickEvent => Dispatcher.UntilCondition<T>(condition);
        
        public Task UntilCondition(Func<bool> condition) => Dispatcher.UntilCondition(condition);

        public void SetEnable(bool enable) {
            if (IsDisposed || IsEnable == enable) {
                return;
            }
            IsEnable = enable;
            if (!(this is IScriptEnable scriptEnable)) {
                return;
            }
            if (IsEnable) {
                scriptEnable.OnScriptDisable();
            }
            else {
                scriptEnable.OnScriptDisable();
            }
        }
        
        public void Dispose() {
            if (IsDisposed) {
                return;
            }
            SetEnable(false);
            if (this is IScriptDestroy scriptDestroy) {
                scriptDestroy.OnScriptDestroy();
            }
            Entity.Remove(GetType());
            IsDisposed = true;
        }
    }
}


// TODO : 增强Script功能，扩展写法
// public class MusicScript : AsyncScript {
//
//     public Sound SoundMusic { get; set; }
//
//     private SoundInstance music;
//     
//     protected override void OnEnable() {
//         if(music == null) {
//             music = SoundMusic.CreateInstance();
//         }
//         music.Play();
//     }
//
//     protected override void OnDisable() {
//         music?.Stop();
//     }
//
//     protected override void OnDestroy() {
//         music?.Dispose();
//     }
// }
//
// public class MusicScript : AsyncScript {
//
//     public Sound SoundMusic { get; set; }
//
//     public override async ValueTask Setup() {
//         using var music = SoundMusic.CreateInstance();
//         OnEnable(() => music.Play());
//         OnDisable(() => music.Stop());
//         await Destroy();
//     }
// }
//
// public class MusicScript : AsyncScript {
//
//     public Sound SoundMusic { get; set; }
//
//     public override async ValueTask Execute() {
//         using var music = SoundMusic.CreateInstance();
//         while(!IsDestroy()){
//             await Enable();
//             music.Play();
//             await Disable();
//             music.Stop();
//         }
//     }
// }
//
// class ScriptComponent : RefComponent {
//
//     protected ScriptSystem Scripts => Sandbox.GetSystem<ScriptSystem>();
//
//     protected ContentManager Contents => Sandbox.Services.Get<ContentManager>();
//
//     protected SpaceComponent Space => Entity.Get<TransformComponent>().Space;
//
//     protected SceneComponent Scene => Space.Entity.Get<SceneComponent>();
//
//     private Logger logger;
//     protected Logger Logger => logger ?? (logger = Sandbox.Services.Get<LogManager>().Get(this.GetType().FullName));
//
//     private ObjectCollector collector;
//     public ObjectCollector Collector => collector ?? collector = new ObjectCollector();
//
//     protected void WhenEnable(Action action) {
//         ScriptSystem.AddCallback(ScriptLifeStage.Enable, this, action)
//     }
//
//     protected void WhenUpdate(Action action) {
//         ScriptSystem.AddCallback(ScriptLifeStage.Enable, this, action)
//     }
//
//     protected void WhenDisable(Action action) {
//         ScriptSystem.AddCallback(ScriptLifeStage.Enable, this, action)
//     }
//
//     public void AddCallback<T>(Func<T> action) {
//         
//     }
//
//     public void AddCallback<T>(Func<T, ValueTask> action) {
//
//     }
// }
//
//
//
//
// class BackgroundMusicScript : ScriptComponent, IScriptAwake {
//     
//     public Sound SoundMusic { get; set; }
//
//     public ValueTask OnAwake() {
//         using(var music = SoundMusic.CreateInstance()) {
//             WhenEnable(() => music.Play());
//             WhenDisable(() => music.Stop());
//             await Scripts.WaitDestroy()
//         }
//     }
// }
