using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Coorth {
    public partial class Sandbox : IDisposable {

        #region Static

        private static readonly List<Sandbox> sandboxes = new List<Sandbox>();
        
        private static readonly object locking = new object();

        private static void OnCreateSandbox(Sandbox sandbox) {
            lock (locking) {
                for (var i = 0; i < sandboxes.Count; i++) {
                    if (sandboxes[i] != null) {
                        continue;
                    }
                    sandbox.Index = i;
                    sandboxes[i] = sandbox;
                    return;
                }
                sandbox.Index = sandboxes.Count;
                sandboxes.Add(sandbox);
            }
        }

        private static void OnRemoveSandbox(Sandbox sandbox) {
            lock (locking) {
                sandboxes[sandbox.Index] = null;
                sandbox.Index = -1;
            }
        }

        public static Sandbox[] GetSandboxes() {
            lock (locking) {
                var array = new Sandbox[sandboxes.Count];
                sandboxes.CopyTo(array);
                return array;
            }
        }
        
        public static Sandbox GetSandbox(int index) {
            lock (locking) {
                if (0 < index && index < sandboxes.Count) {
                    return sandboxes[index];
                }
                return null;
            }
        }

        #endregion

        #region Instance

        public readonly string Name;

        public int Index { get; private set; }

        public bool IsDisposed { get; private set; }

        public readonly SandboxConfig Config;

        public readonly SandboxContext Context;

        public readonly EventDispatcher Dispatcher;

        public readonly IServiceLocator Services;
        
        public Sandbox( SandboxConfig config = null, 
                        IServiceLocator services = null, 
                        EventDispatcher dispatcher = null) {

            OnCreateSandbox(this);
            
            this.Config = config ?? SandboxConfig.Default;
            this.Name = this.Config.Name ?? $"Sandbox_{Index.ToString()}";
            this.Services = services ?? new ServiceLocator();
            this.Dispatcher = dispatcher ?? new EventDispatcher();
            this.Context = new SandboxContext();
            
            InitArchetypes(Config.ArchetypeCapacity.Index, Config.ArchetypeCapacity.Chunk);
            InitEntities(Config.EntityCapacity.Index, Config.EntityCapacity.Chunk);
            InitComponents(Config.ComponentGroupCapacity, Config.ComponentDataCapacity.Index, Config.ComponentDataCapacity.Chunk);
            InitSystems();
            
            singleton = CreateEntity();
        }
        
        public void Dispose() {
            if (IsDisposed) {
                return;
            }
            ClearEntities();
            ClearComponents();
            ClearSystems();
            ClearArchetypes();
            IsDisposed = true;
            OnRemoveSandbox(this);
        }
        
        public object GetService(Type serviceType) {
            return Services.GetService(serviceType);
        }

        public T GetService<T>() where T : class {
            return Services.GetService<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispatch<T>(in T e = default) {
            Dispatcher.Dispatch(e);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ValueTask DispatchAsync<T>(in T e = default) {
            return Dispatcher.DispatchAsync(e);
        }
        
        public void Execute<T>(in T e = default) {
            Dispatcher.Dispatch(e);
        }
        
        public ValueTask ExecuteAsync<T>(in T e = default) {
            return Dispatcher.DispatchAsync(e);
        }
        
        #endregion
    }
}