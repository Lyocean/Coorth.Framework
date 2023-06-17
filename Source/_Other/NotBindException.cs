using System;

namespace Coorth; 

public class NotBindException : Exception {
        
    public NotBindException() {}

    public NotBindException(Type type) : base(type.ToString()) {
    }
}