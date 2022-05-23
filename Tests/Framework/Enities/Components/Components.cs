using System.Numerics;

namespace Coorth.Framework; 

public struct TestValueComponent0 : IComponent {
}
    
public struct TestValueComponent1 : IComponent {
    public int a;
    public int b;
}
    
public struct TestValueComponent2 : IComponent {
    public float a;
    public float b;
    public float c;
}
    
public struct TestValueComponent3 : IComponent {
    public Vector4 v1;
    public Vector4 v2;
}
    
public struct TestValueComponent4 : IComponent {
    public Vector4 v1;
    public Vector4 v2;
    public Vector4 v3;
    public Vector4 v4;
}
    
public class TestClassComponent0 : IComponent {
}
    
public class TestClassComponent1 : IComponent {
    public int a;
    public int b;
}
    
public class TestClassComponent2 : IComponent {
    public float a;
    public float b;
    public float c;
}
    
public class TestClassComponent3 : IComponent {
    public Vector4 v1;
    public Vector4 v2;
}
    
public class TestClassComponent4 : IComponent {
    public Vector4 v1;
    public Vector4 v2;
    public Vector4 v3;
    public Vector4 v4;
}