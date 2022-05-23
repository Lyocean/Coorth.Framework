using Coorth.Logs;
using Coorth.Framework;

namespace Coorth.Framework; 

public readonly record struct AppOptions(int Id, string Name, ILogger Logger, Dispatcher? Dispatcher, ServiceLocator? Services) {

    public readonly int Id = Id;

    public readonly string Name = Name;

    public readonly ILogger Logger = Logger;

    public readonly Dispatcher? Dispatcher = Dispatcher;

    public readonly ServiceLocator? Services = Services;
}