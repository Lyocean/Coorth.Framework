using System;


namespace Coorth.Framework; 

public class ServiceException : Exception {

    public ServiceException(string content) : base(content) {
        
    }

}