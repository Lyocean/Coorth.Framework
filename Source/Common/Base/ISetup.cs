namespace Coorth;

public interface ISetup {
    void OnSetup();
}

public interface ISetup<in T> {
    void OnSetup(T p);
}
    
public interface ISetup<in T1, in T2> {
    void OnSetup(T1 p1, T2 p2);
}
    
public interface ISetup<in T1, in T2, in T3> {
    void OnSetup(T1 p1, T2 p2, T3 p3);
}
    
public interface ISetup<in T1, in T2, in T3, in T4> {
    void OnSetup(T1 p1, T2 p2, T3 p3, T4 p4);
}
    
public interface ISetup<in T1, in T2, in T3, in T4, in T5> {
    void OnSetup(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);
}