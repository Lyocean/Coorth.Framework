namespace Coorth.Graphs; 

public struct DataNodeRuntime<TContext> {
    public DataNodeDefinition<TContext> Definition;
    public int Id;
    public short ReaderCount;
    public short WriterIndex;
    public short RefCount;
    public short First;
    public short Last;
}