namespace Coorth {
    public interface ICloneTo<T> {
        void CloneTo(ref T target);
    }

    public interface IAwake {
        void OnAwake();
    }

    public interface IAwake<in T> {
        void OnAwake(T p);
    }
    
    public interface IAwake<in T1, in T2> {
        void OnAwake(T1 p1, T2 p2);
    }
    
    public interface IAwake<in T1, in T2, in T3> {
        void OnAwake(T1 p1, T2 p2, T3 p3);
    }
    
    public interface IAwake<in T1, in T2, in T3, in T4> {
        void OnAwake(T1 p1, T2 p2, T3 p3, T4 p4);
    }
    
    public interface IAwake<in T1, in T2, in T3, in T4, in T5> {
        void OnAwake(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);
    }
    
    public interface ISandboxEvent : IEvent { }
}