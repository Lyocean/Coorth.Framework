namespace Coorth; 

public interface ILifetime {
    
    void OnAdd();
    
    void OnSetup();
    
    void OnActive();
    
    void OnDeActive();
    
    void OnClear();
    
    void OnRemove();
}