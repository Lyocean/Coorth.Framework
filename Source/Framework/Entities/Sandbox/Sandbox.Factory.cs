using System;
using System.Collections.Generic;

namespace Coorth.Framework; 

public partial class Sandbox {

    private readonly Dictionary<Type, EntityFactory> factories = new();

    public EntityFactory BindFactory(Type key, EntityFactory factory) {
        factories.Add(key, factory);
        factory.Setup();
        return factory;
    }

    public TFactory GetFactory<TFactory>() where TFactory : EntityFactory => (TFactory)factories[typeof(TFactory)];
}