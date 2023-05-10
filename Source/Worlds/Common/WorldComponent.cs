namespace Coorth.Framework; 

[Component, Guid("28917B42-D4B2-4080-9890-98193F6C897A")]
public sealed class WorldComponent : Component {

    public readonly WorldsModule Module;

    public readonly AppBase App;

    public WorldComponent(WorldsModule module, AppBase app) {
        Module = module;
        App = app;
    }
}