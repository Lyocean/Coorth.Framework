namespace Coorth.Framework; 

public readonly struct PortData {
    public readonly int EdgeMin;
    public readonly int EdgeMax;

    public PortData(int edgeMin, int edgeMax) {
        EdgeMin = edgeMin;
        EdgeMax = edgeMax;
    }

    public static PortData Compile(PortDefinition definition, ref int index) {
        var data = new PortData(
            edgeMin: (short)index, 
            edgeMax:(short)(index + definition.Edges.Count - 1));
        index += definition.Edges.Count;
        return data;
    }
}