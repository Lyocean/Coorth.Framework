using System;

namespace Coorth.Framework; 

public interface IMessage {
}
    
public interface IRequest : IMessage {
}

public interface IResponse : IMessage {
}