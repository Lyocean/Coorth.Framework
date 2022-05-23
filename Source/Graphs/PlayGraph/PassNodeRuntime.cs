using System;

namespace Coorth.Graphs; 

public struct PassNodeRuntime<TContext> {
    public PassNodeDefinition<TContext> Definition;
    public int RefCount;
    public ArraySegment<int> Inputs;
    public ArraySegment<int> Outputs;
    public ArraySegment<int> Initial;
    public ArraySegment<int> Release;
}