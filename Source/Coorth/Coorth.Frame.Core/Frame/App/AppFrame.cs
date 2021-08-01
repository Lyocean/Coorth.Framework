﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;

namespace Coorth {
    public abstract class AppFrame : Disposable, ISupportInitialize {
        
        #region Fields

        public readonly AppConfig Config;

        private bool isRunning = false;

        public bool IsRunning => isRunning;
        
        public int MainThreadId { get; private set; }

        public readonly EventDispatcher Dispatcher = new EventDispatcher();

        public readonly ActorContainer Actors;
        
        public Infra Infra => Infra.Instance;
        public ServiceContainer Services => Infra.Services;
        

        public World World { get; private set; }

        private readonly List<World> worlds = new List<World>();
        public IReadOnlyList<World> Worlds => worlds;

        public RootModule Module { get; private set; } = new RootModule();

        #endregion

        #region Lifecycle

        protected AppFrame(AppConfig config = null) {
            this.Config = config ?? AppConfig.Default;
            this.Actors = new ActorContainer(this.Dispatcher, this.Config);

            var moduleServices = new ServiceContainer();
            Services.AddChild(moduleServices);
            Dispatcher.AddChild(moduleServices.Dispatcher);
            this.Module.ServiceAdd(moduleServices);
        }
        
        public virtual void Setup() {
            
            SetupInfra();
            SetupWorld();
            SetupModule();
            
            OnSetup();
        }

        protected virtual void OnSetup() { }

        public virtual void BeginInit() {
            Execute(new EventAppBeginInit());
        }

        public virtual void EndInit() {
            Execute(new EventAppEndInit());
        }

        public bool Startup() {
            if (isRunning) {
                return false;
            }
            isRunning = true;
            MainThreadId = Thread.CurrentThread.ManagedThreadId;
            OnStartup();
            Dispatcher.Execute(new EventAppStartup(MainThreadId));
            return true;
        }
        
        protected virtual void OnStartup() { }
        
        public void Execute<T>(in T e) where T: IEvent {
            if (!IsDisposed) {
                return;
            }

            try {
                Dispatcher.Execute(e);
            } catch (Exception ex) {
                LogUtil.Exception(ex);
            }
        }

        public void Shutdown() {
            if (!isRunning) {
                return;
            }
            isRunning = false;
            Dispatcher.Execute(new EventAppShutdown());
            OnShutdown();
        }
        
        protected virtual void OnShutdown() { }
        
        protected override void Dispose(bool dispose) {
            Shutdown();
            OnDispose();
        }
        
        protected virtual void OnDispose() { }
        
        #endregion
        
        #region Infra

        protected virtual void SetupInfra() { }

        #endregion
        
        #region World
        
        public World CreateWorld(WorldConfig config) {
            var world = new World(this, config);
            worlds.Add(world);
            if(this.World == null) {
                this.World = world;
            }
            if (IsRunning) {
                this.World.Execute(new EventAppStartup(MainThreadId));
            }
            return world;
        }

        protected virtual WorldConfig GetDefaultWorldConfig() {
            return new WorldConfig();
        }
        
        protected virtual void SetupWorld() {
            var config = GetDefaultWorldConfig();
            var world = CreateWorld(config);

            var sandbox = world.Active;
            sandbox.AddSystem<CoreSystems>();
        }
        
        #endregion

        #region Module

        protected virtual void SetupModule() { }

        #endregion

    }


}
