namespace Quantities.Test;
public static class Convenience
{
    public static Int32 SiPrecision => 15;
    public static Int32 ImperialPrecision => 14;
    public static void Matches(this Length actual, Length expected)
    {
        actual.Matches(expected, SiPrecision);
    }
    public static void Matches(this Length actual, Length expected, Int32 precision)
    {
        Assert.Equal((Double)expected, (Double)actual, precision);
    }
    public static void Matches(this Area actual, Area expected)
    {
        actual.Matches(expected, SiPrecision);
    }
    public static void Matches(this Area actual, Area expected, Int32 precision)
    {
        Assert.Equal((Double)expected, (Double)actual, precision);
    }
}