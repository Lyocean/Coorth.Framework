using System;
using System.Collections.Generic;

namespace Coorth {
    public partial class Sandbox {

        private readonly Dictionary<Type, EntityFactory> factories = new Dictionary<Type, EntityFactory>();

        public TFactory BindFactory<TFactory>() where TFactory : EntityFactory, new() {
            var factory = new TFactory();
            factories.Add(typeof(TFactory), factory);
            factory.Setup(this);
            return factory;
        }

        public TFactory OfferFactory<TFactory>() where TFactory : EntityFactory, new() {
            if (factories.TryGetValue(typeof(TFactory), out var factory)) {
                return (TFactory)factory;
            }
            return BindFactory<TFactory>();
        }
    }
}