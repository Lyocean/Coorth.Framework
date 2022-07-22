namespace Coorth.Framework; 

public interface IBehaviour {
    bool Enter(object context);
}

public interface IBehaviour<TContext> {
    bool Enter(ref TContext context);
    
}