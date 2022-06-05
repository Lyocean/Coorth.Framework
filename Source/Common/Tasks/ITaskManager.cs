using Coorth.Framework;

namespace Coorth.Tasks; 

[Manager]
public interface ITaskManager : IManager {
    void Setup();
}

public class TaskManager : Manager, ITaskManager {
    public void Setup() { }
}