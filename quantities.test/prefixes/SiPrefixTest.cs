namespace Quantities.Test.Prefixes;

public class SiPrefixTest
{
    private static readonly IPrefixInject<Double> injector = new GetValue();

    [Theory]
    [MemberData(nameof(SiExponents))]
    public void ScaleSiPrefixes(Int32 exponent)
    {
        Double valueToNormalize = Math.Pow(10, exponent);
        Double actualValue = SiPrefix.Scale(valueToNormalize, injector);
        Double actualExponent = (Int32)Math.Log10(actualValue);

        Assert.Equal(0, actualExponent);
    }
    [Theory]
    [MemberData(nameof(SiExponents))]
    public void ScalesAllValuesWithinOneAndThousand(Int32 exponent)
    {
        var seed = Math.Sqrt(2) * Math.E / (2d * Math.E);
        var range = new Double[] { (1d + Math.Pow(10, -12)) / seed, 2, 4, 5, 6, 8, 9 }.Select(r => seed * r).ToArray();
        var inputValues = Enumerable.Range(exponent, 3).SelectMany(e => range.Select(r => r * Math.Pow(10, e))).ToArray();

        var actual = inputValues.Select(v => SiPrefix.Scale(v, injector)).ToArray();

        Assert.All(actual, a => Assert.InRange(a, 1d, 1000d));
    }

    [Theory]
    [MemberData(nameof(SmallPowerThreeExponents))]
    public void ScaleSmallPowerOfThreePrefixes(Int32 exponent)
    {
        Int32[] expectedExponents = new[] { 0, 1, 2 };

        var valuesToNormalize = Enumerable.Range(exponent, expectedExponents.Length).Select(e => Math.Pow(10, e)).ToArray();
        var actualValues = valuesToNormalize.Select(v => SiPrefix.Scale(v, injector)).ToArray();
        var actualExponents = actualValues.Select(v => (Int32)Math.Log10(v)).ToArray();

        Assert.Equal(expectedExponents, actualExponents);
    }

    [Theory]
    [MemberData(nameof(LargePowerThreeExponents))]
    public void ScaleLargePowerOfThreePrefixes(Int32 exponent)
    {
        Int32[] expectedExponents = new[] { 0, 1, 2 };
        var valuesToNormalize = Enumerable.Range(exponent, expectedExponents.Length).Select(e => Math.Pow(10, e)).ToArray();
        var actualValues = valuesToNormalize.Select(v => SiPrefix.Scale(v, injector)).ToArray();
        var actualExponents = actualValues.Select(v => (Int32)Math.Log10(v)).ToArray();

        Assert.Equal(expectedExponents, actualExponents);
    }

    [Theory]
    [MemberData(nameof(VerySmallExponents))]
    public void ScaleVerySmallValues(Int32 exponent)
    {
        Double valueToNormalize = Math.Pow(10, exponent);
        Double actualValue = SiPrefix.Scale(valueToNormalize, injector);
        Double actualExponent = (Int32)Math.Log10(actualValue);

        Int32 expectedExponent = exponent - Yocto.Exp;
        Assert.Equal(expectedExponent, actualExponent);
    }

    [Theory]
    [MemberData(nameof(VeryLargeExponents))]
    public void ScaleVeryLargeValues(Int32 exponent)
    {
        Double valueToNormalize = Math.Pow(10, exponent);
        Double actualValue = SiPrefix.Scale(valueToNormalize, injector);
        Double actualExponent = (Int32)Math.Log10(actualValue);

        Int32 expectedExponent = exponent - Yotta.Exp;
        Assert.Equal(expectedExponent, actualExponent);
    }

    public static IEnumerable<Object[]> SiExponents()
    {
        return AllSiExponents().Select(i => new Object[] { i });
    }

    public static IEnumerable<Object[]> LargePowerThreeExponents()
    {
        return AllSiExponents().Where(e => e >= Kilo.Exp).Select(i => new Object[] { i });
    }
    public static IEnumerable<Object[]> SmallPowerThreeExponents()
    {
        return AllSiExponents().Where(e => e < Milli.Exp).Select(i => new Object[] { i });
    }

    public static IEnumerable<Object[]> VeryLargeExponents()
    {
        return Enumerable.Range(Yotta.Exp, 5).Select(i => new Object[] { i });
    }

    public static IEnumerable<Object[]> VerySmallExponents()
    {
        return Enumerable.Range(0, 5).Select(i => new Object[] { Yocto.Exp - i });
    }

    private static IEnumerable<Int32> AllSiExponents()
    {
        yield return Yotta.Exp;
        yield return Zetta.Exp;
        yield return Exa.Exp;
        yield return Peta.Exp;
        yield return Tera.Exp;
        yield return Giga.Exp;
        yield return Mega.Exp;
        yield return Kilo.Exp;
        yield return Hecto.Exp;
        yield return Deca.Exp;
        yield return UnitPrefix.Exp;
        yield return Deci.Exp;
        yield return Centi.Exp;
        yield return Milli.Exp;
        yield return Micro.Exp;
        yield return Nano.Exp;
        yield return Pico.Exp;
        yield return Femto.Exp;
        yield return Atto.Exp;
        yield return Zepto.Exp;
        yield return Yocto.Exp;
    }

    private sealed class GetValue : IPrefixInject<Double>
    {
        public Double Identity(in Double value) => value;
        public Double Inject<TPrefix>(in Double value) where TPrefix : IPrefix => value;
    }
}
