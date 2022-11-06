namespace Quantities.Measures;
public interface IMeasure
{
    static abstract Quant Create(in Double value);
}

internal abstract class Map
{
    public abstract Double Value<TKernel>(in Double value) where TKernel : IKernel;
    public abstract Double Project(Map map, in Double value);
}

internal sealed class Map<TKernel> : Map
    where TKernel : IKernel
{
    public override Double Value<TOtherKernel>(in Double value) => TKernel.Map<TOtherKernel>(in value);
    public override Double Project(Map map, in Double value) => map.Value<TKernel>(in value);
}