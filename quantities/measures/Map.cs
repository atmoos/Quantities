namespace Quantities.Measures;

internal abstract class Map
{
    public abstract Double Value<TKernel>(in Double value) where TKernel : IKernel;
    public abstract Double Project(Map map, in Double value);
    public abstract T Inject<T>(in Creator<T> creator);
    public abstract String Append(String value);
}

internal sealed class Map<TKernel> : Map
    where TKernel : IKernel, IRepresentable
{
    public override Double Value<TOtherKernel>(in Double value) => TOtherKernel.Map<TKernel>(in value);
    public override Double Project(Map map, in Double value) => map.Value<TKernel>(in value);
    public override T Inject<T>(in Creator<T> creator) => TKernel.Inject(in creator);
    public override String Append(String value) => $"{value} {TKernel.Representation}";
}