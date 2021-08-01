namespace Coorth {
    public interface IScriptLifecycle {
        bool IsDisposed { get; }
        bool IsEnable { get; }
    }
    
    public interface IScriptStart : IScriptLifecycle {
        void OnScriptStart();
    }
    
    public interface IScriptEnable : IScriptLifecycle {
        void OnScriptEnable();
        void OnScriptDisable();
    }
    
    public interface IScriptStepUpdate : IScriptLifecycle {
        void OnStepUpdate(in EventStepUpdate e);
    }
    
    public interface IScriptTickUpdate : IScriptLifecycle {
        void OnTickUpdate(in EventTickUpdate e);
    }

    public interface IScriptLateUpdate : IScriptLifecycle {
        void OnLateUpdate(in EventLateUpdate e);
    }
    
    public interface IScriptDestroy : IScriptLifecycle {
        void OnScriptDestroy();
    }
}