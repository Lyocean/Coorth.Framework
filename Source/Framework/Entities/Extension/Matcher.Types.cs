
namespace Coorth.Framework; 

public partial class Matcher {
  
    public Matcher WithAll<T0>() where T0 : IComponent {
        allTypes = ComponentTypes<T0>.Types;
        return this;
    }
    
    public Matcher WithNot<T0>() where T0 : IComponent {
        notTypes = ComponentTypes<T0>.Types;
        return this;
    }

    public Matcher WithAny<T0>() where T0 : IComponent {
        anyTypes = ComponentTypes<T0>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1>() where T0 : IComponent where T1 : IComponent {
        allTypes = ComponentTypes<T0, T1>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1>() where T0 : IComponent where T1 : IComponent {
        notTypes = ComponentTypes<T0, T1>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1>() where T0 : IComponent where T1 : IComponent {
        anyTypes = ComponentTypes<T0, T1>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1, T2>() where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        allTypes = ComponentTypes<T0, T1, T2>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1, T2>() where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        notTypes = ComponentTypes<T0, T1, T2>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1, T2>() where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        anyTypes = ComponentTypes<T0, T1, T2>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1, T2, T3>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        allTypes = ComponentTypes<T0, T1, T2, T3>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1, T2, T3>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        notTypes = ComponentTypes<T0, T1, T2, T3>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1, T2, T3>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        anyTypes = ComponentTypes<T0, T1, T2, T3>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1, T2, T3, T4>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        allTypes = ComponentTypes<T0, T1, T2, T3, T4>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        notTypes = ComponentTypes<T0, T1, T2, T3, T4>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1, T2, T3, T4>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        anyTypes = ComponentTypes<T0, T1, T2, T3, T4>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1, T2, T3, T4, T5>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        allTypes = ComponentTypes<T0, T1, T2, T3, T4, T5>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        notTypes = ComponentTypes<T0, T1, T2, T3, T4, T5>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        anyTypes = ComponentTypes<T0, T1, T2, T3, T4, T5>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1, T2, T3, T4, T5, T6>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        allTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5, T6>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        notTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5, T6>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        anyTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        allTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        notTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        anyTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1, T2, T3, T4, T5, T6, T7, T8>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        allTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5, T6, T7, T8>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        notTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5, T6, T7, T8>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        anyTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Types;
        return this;
    }

    public Matcher WithAll<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        allTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Types;
        return this;
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        notTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Types;
        return this;
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        anyTypes = ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Types;
        return this;
    }

}