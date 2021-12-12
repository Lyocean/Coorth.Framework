using System;
using System.Collections.Generic;

namespace Coorth {
    public partial class Sandbox {

        private readonly Dictionary<Type, IEntityFactory> factories = new Dictionary<Type, IEntityFactory>();

        public TFactory BindFactory<TFactory>() where TFactory : class, IEntityFactory, new() {
            var factory = new TFactory();
            factories.Add(typeof(TFactory), factory);
            return factory;
        }

        public TFactory OfferFactory<TFactory>() where TFactory : class, IEntityFactory, new() {
            if (factories.TryGetValue(typeof(TFactory), out var factory)) {
                return (TFactory)factory;
            }
            return BindFactory<TFactory>();
        }
    }
}