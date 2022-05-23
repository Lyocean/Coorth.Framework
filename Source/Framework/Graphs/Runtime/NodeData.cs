namespace Coorth.Framework; 

public readonly struct NodeData {
    public readonly NodeDefinition Definition;
    public readonly int InputMin;
    public readonly int InputMax;
    public readonly int OutputMin;
    public readonly int OutputMax;

    public NodeData(NodeDefinition definition, int inputMin, int inputMax, int outputMin, int outputMax) {
        this.Definition = definition;
        this.InputMin = inputMin;
        this.InputMax = inputMax;
        this.OutputMin = outputMin;
        this.OutputMax = outputMax;
    }
}