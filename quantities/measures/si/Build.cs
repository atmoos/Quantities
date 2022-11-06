namespace Quantities.Measures.Si;
public static class Build<TSi> where TSi : ISi
{
    private static readonly Map map = new Map<SiKernel<TSi>>();
    public static Quant With(in Double value) => new(value, in map);
    public static Quant With(in Quant value)
    {
        var siValue = value.Project<SiKernel<TSi>>();
        return new(in siValue, in map);
    }
}