using System.Threading;
using Coorth.Logs;
using Coorth.Tasks;

namespace Coorth.Framework; 

public record WorldOptions {

    public string Name { get; set; } = "Worlds-Default";

    public (int Index, int Chunk) EntityCapacity { get; set; } = (1024, 1024);
        
    public (int Index, int Chunk) ArchetypeCapacity { get; set; } = (64, 64);

    public int ComponentGroupCapacity { get; set; } = 32;

    public (int Index, int Chunk) ComponentDataCapacity { get; set; } = (2, 128);

    private IServiceLocator? services;
    public IServiceLocator Services { get => services ??= new ServiceLocator(); set => services = value; }
    
    private ILogger? logger;
    public ILogger Logger { get => logger ??= new LoggerConsole(); set => logger = value; }
    
    private Dispatcher? dispatcher;
    public Dispatcher Dispatcher { get => dispatcher ??= new Dispatcher(null!); set => dispatcher = value; }

    private TaskSyncContext? schedule;
    public TaskSyncContext SyncContext { get => schedule ?? new TaskSyncContext(Thread.CurrentThread); set => schedule = value; }
    
}