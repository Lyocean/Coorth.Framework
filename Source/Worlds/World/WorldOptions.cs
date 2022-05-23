using Coorth.Framework;

namespace Coorth.Worlds; 

public readonly record struct WorldOptions(string? Name, AppFrame App, WorldsModule Module, IServiceLocator Services) {

    public readonly string? Name = Name;

    public readonly AppFrame App = App;

    public readonly WorldsModule Module = Module;
    
    public readonly IServiceLocator Services = Services;

}