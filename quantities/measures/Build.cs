namespace Quantities.Measures;

internal static class Build<TMeasure> where TMeasure : IMeasure
{
    private static readonly Map map = new Map<TMeasure>();
    public static Quant With(in Double value) => new(value, in map);
    public static Quant With(in Quant value)
    {
        var projection = value.Project<TMeasure>();
        return new(in projection, in map);
    }
}