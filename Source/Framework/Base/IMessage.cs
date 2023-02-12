using System;

namespace Coorth.Framework; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class MessageAttribute : Attribute { }

public interface IMessage { }
    
public interface IRequest : IMessage { }

public interface IResponse : IMessage { }