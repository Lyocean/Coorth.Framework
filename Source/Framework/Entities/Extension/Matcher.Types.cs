
namespace Coorth.Framework; 

public partial class Matcher {
  
    public Matcher WithAll<T0>() where T0 : IComponent {
        return WithAll(ComponentTypes<T0>.Types);
    }
    
    public Matcher WithNot<T0>() where T0 : IComponent {
        return WithNot(ComponentTypes<T0>.Types);
    }

    public Matcher WithAny<T0>() where T0 : IComponent {
        return WithAny(ComponentTypes<T0>.Types);
    }

    private struct AllMatcher<T0> where T0 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0>();
        }
    }
    
    public static Matcher All<T0>() where T0 : IComponent {
        return AllMatcher<T0>.Instance;
    }
    
    public Matcher WithAll<T0, T1>() where T0 : IComponent where T1 : IComponent {
        return WithAll(ComponentTypes<T0, T1>.Types);
    }
    
    public Matcher WithNot<T0, T1>() where T0 : IComponent where T1 : IComponent {
        return WithNot(ComponentTypes<T0, T1>.Types);
    }

    public Matcher WithAny<T0, T1>() where T0 : IComponent where T1 : IComponent {
        return WithAny(ComponentTypes<T0, T1>.Types);
    }

    private struct AllMatcher<T0, T1> where T0 : IComponent where T1 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1>();
        }
    }
    
    public static Matcher All<T0, T1>() where T0 : IComponent where T1 : IComponent {
        return AllMatcher<T0, T1>.Instance;
    }
    
    public Matcher WithAll<T0, T1, T2>() where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        return WithAll(ComponentTypes<T0, T1, T2>.Types);
    }
    
    public Matcher WithNot<T0, T1, T2>() where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        return WithNot(ComponentTypes<T0, T1, T2>.Types);
    }

    public Matcher WithAny<T0, T1, T2>() where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        return WithAny(ComponentTypes<T0, T1, T2>.Types);
    }

    private struct AllMatcher<T0, T1, T2> where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1, T2>();
        }
    }
    
    public static Matcher All<T0, T1, T2>() where T0 : IComponent where T1 : IComponent where T2 : IComponent {
        return AllMatcher<T0, T1, T2>.Instance;
    }
    
    public Matcher WithAll<T0, T1, T2, T3>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        return WithAll(ComponentTypes<T0, T1, T2, T3>.Types);
    }
    
    public Matcher WithNot<T0, T1, T2, T3>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        return WithNot(ComponentTypes<T0, T1, T2, T3>.Types);
    }

    public Matcher WithAny<T0, T1, T2, T3>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        return WithAny(ComponentTypes<T0, T1, T2, T3>.Types);
    }

    private struct AllMatcher<T0, T1, T2, T3> where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1, T2, T3>();
        }
    }
    
    public static Matcher All<T0, T1, T2, T3>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent {
        return AllMatcher<T0, T1, T2, T3>.Instance;
    }
    
    public Matcher WithAll<T0, T1, T2, T3, T4>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        return WithAll(ComponentTypes<T0, T1, T2, T3, T4>.Types);
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        return WithNot(ComponentTypes<T0, T1, T2, T3, T4>.Types);
    }

    public Matcher WithAny<T0, T1, T2, T3, T4>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        return WithAny(ComponentTypes<T0, T1, T2, T3, T4>.Types);
    }

    private struct AllMatcher<T0, T1, T2, T3, T4> where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1, T2, T3, T4>();
        }
    }
    
    public static Matcher All<T0, T1, T2, T3, T4>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent {
        return AllMatcher<T0, T1, T2, T3, T4>.Instance;
    }
    
    public Matcher WithAll<T0, T1, T2, T3, T4, T5>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        return WithAll(ComponentTypes<T0, T1, T2, T3, T4, T5>.Types);
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        return WithNot(ComponentTypes<T0, T1, T2, T3, T4, T5>.Types);
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        return WithAny(ComponentTypes<T0, T1, T2, T3, T4, T5>.Types);
    }

    private struct AllMatcher<T0, T1, T2, T3, T4, T5> where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1, T2, T3, T4, T5>();
        }
    }
    
    public static Matcher All<T0, T1, T2, T3, T4, T5>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent {
        return AllMatcher<T0, T1, T2, T3, T4, T5>.Instance;
    }
    
    public Matcher WithAll<T0, T1, T2, T3, T4, T5, T6>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        return WithAll(ComponentTypes<T0, T1, T2, T3, T4, T5, T6>.Types);
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5, T6>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        return WithNot(ComponentTypes<T0, T1, T2, T3, T4, T5, T6>.Types);
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5, T6>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        return WithAny(ComponentTypes<T0, T1, T2, T3, T4, T5, T6>.Types);
    }

    private struct AllMatcher<T0, T1, T2, T3, T4, T5, T6> where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1, T2, T3, T4, T5, T6>();
        }
    }
    
    public static Matcher All<T0, T1, T2, T3, T4, T5, T6>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent {
        return AllMatcher<T0, T1, T2, T3, T4, T5, T6>.Instance;
    }
    
    public Matcher WithAll<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        return WithAll(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7>.Types);
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        return WithNot(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7>.Types);
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        return WithAny(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7>.Types);
    }

    private struct AllMatcher<T0, T1, T2, T3, T4, T5, T6, T7> where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1, T2, T3, T4, T5, T6, T7>();
        }
    }
    
    public static Matcher All<T0, T1, T2, T3, T4, T5, T6, T7>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent {
        return AllMatcher<T0, T1, T2, T3, T4, T5, T6, T7>.Instance;
    }
    
    public Matcher WithAll<T0, T1, T2, T3, T4, T5, T6, T7, T8>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        return WithAll(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Types);
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5, T6, T7, T8>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        return WithNot(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Types);
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5, T6, T7, T8>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        return WithAny(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Types);
    }

    private struct AllMatcher<T0, T1, T2, T3, T4, T5, T6, T7, T8> where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1, T2, T3, T4, T5, T6, T7, T8>();
        }
    }
    
    public static Matcher All<T0, T1, T2, T3, T4, T5, T6, T7, T8>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent {
        return AllMatcher<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Instance;
    }
    
    public Matcher WithAll<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        return WithAll(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Types);
    }
    
    public Matcher WithNot<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        return WithNot(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Types);
    }

    public Matcher WithAny<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        return WithAny(ComponentTypes<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Types);
    }

    private struct AllMatcher<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        public static readonly Matcher Instance;
        
        static AllMatcher() {
            Instance = (new Matcher()).WithAll<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        }
    }
    
    public static Matcher All<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T0 : IComponent where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent where T5 : IComponent where T6 : IComponent where T7 : IComponent where T8 : IComponent where T9 : IComponent {
        return AllMatcher<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Instance;
    }
    
}