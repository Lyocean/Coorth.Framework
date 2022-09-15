namespace Coorth.Framework; 

public interface IBehaviour {
    bool Enter(object context);
    bool Execute();
    void Exit();
}

public interface IBehaviour<TContext> {
    bool Enter(ref TContext context);
    bool Execute(ref TContext context);
    void Exit(ref TContext context);
}