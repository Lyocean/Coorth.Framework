using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coorth.Framework; 

public class NodeDefinition {
    
    public static readonly NodeDefinition Empty = new();

    public Guid Id { get; } = Guid.NewGuid();

    public int Index { get; internal set; } = -1;

    public GraphDefinition Graph { get; internal set; } = GraphDefinition.Empty;

    private readonly List<PortDefinition> inPorts = new();
    public IReadOnlyList<PortDefinition> InPorts => inPorts;

    private readonly List<PortDefinition> outPorts = new();
    public IReadOnlyList<PortDefinition> OutPorts => outPorts;

    public int InDegree => InPorts.Sum(port => port.Edges.Count);

    public int OutDegree => OutPorts.Sum(port => port.Edges.Count);
        
    public int MaxNodePortCount => Graph.MaxNodePortCount;

    public int MaxPortEdgeCount => Graph.MaxPortEdgeCount;

    public void AddInPort(PortDefinition port) {
        port.Setup(this, inPorts.Count);
        inPorts.Add(port);
    }
    
    public void AddInPort<T>() where T : PortDefinition, new() {
        AddInPort(new T());
    }
        
    public void AddOutPort(PortDefinition port) {
        port.Setup(this, outPorts.Count);
        outPorts.Add(port);
    }
        
    public void AddOutPort<T>() where T : PortDefinition, new() {
        AddOutPort(new T());
    }

    public NodeData Compile(ref int index) {
        var inputMin = inPorts.Count > 0 ? index : -1;
        var inputMax = inPorts.Count > 0 ? inputMin + inPorts.Count - 1 : -1;
        var outputMin = OutPorts.Count > 0 ? inputMax + 1 : -1;
        var outputMax = OutPorts.Count > 0 ? outputMin + inPorts.Count - 1 : -1;
        var data = new NodeData(this, inputMin, inputMax, outputMin, outputMax);
        index = index + inPorts.Count + OutPorts.Count;
        return data;
    }
}

public class NodeDefinition<TContext> : NodeDefinition where TContext : IGraphContext {
        
    public virtual void OnBuild(ref TContext context) { }
        
    public virtual void OnInit(ref TContext context) { }
        
    public virtual void OnEnter(ref TContext context) { }

    public virtual NodeStatus OnExecute(ref TContext context) => NodeStatus.Success;

    public virtual Task OnExecuteAsync(TContext context) => Task.CompletedTask;

    public virtual void OnExit(ref TContext context) { }

    public virtual void OnClear(ref TContext context) {}

}