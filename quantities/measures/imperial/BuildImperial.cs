namespace Quantities.Measures.Imperial;
public static class BuildImperial<TImperial>
    where TImperial : ITransform, IRepresentable
{
    private static readonly Map map = new Map<OtherKernel<TImperial>>();
    public static Quant With(in Double value) => new(value, in map);
    public static Quant With(in Quant value)
    {
        var siValue = value.Project<OtherKernel<TImperial>>();
        return new(in siValue, in map);
    }
}