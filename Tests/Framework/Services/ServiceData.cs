namespace Coorth.Framework; 

public interface IServiceApi1 {
    
}

public class ServiceImpl1 : IServiceApi1 {
    
}

public interface IServiceApi2 {
    int Value { get; set; }
}

public sealed class ServiceImpl2 : IServiceApi2 {
    public int Value { get; set; }
}

public sealed class ServiceInst1 {
}

public sealed class ServiceInst2 {
    public int Value { get; set; }
}