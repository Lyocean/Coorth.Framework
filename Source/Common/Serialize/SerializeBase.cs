using System;
using System.Collections.Generic;

namespace Coorth {

    public enum SerializeScope {
        None,
        Tuple,
        Struct,
        Class,
    }
    
    public abstract class SerializeBase {

        #region Common

        private readonly Dictionary<Type, object> contexts = new Dictionary<Type, object>();

        protected string error;

        public string GetError() => error;

        public void SetContext<T>(T value) => contexts[typeof(T)] = value;

        public T GetContext<T>() => (T)contexts[typeof(T)];
        
        public abstract void EndRoot();
        public abstract void EndScope();
        public abstract bool EndList();
        public abstract bool EndDict();

        #endregion
    }
}