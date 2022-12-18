namespace Quantities.Test.Prefixes;

public interface ITestPrefix
{
    Double Factor { get; }
    Double ToSi(Double value);
    Double FromSi(Double value);
}

public sealed class TestPrefix<TPrefix> : ITestPrefix
    where TPrefix : IPrefix
{
    public Double Factor { get; }
    public TestPrefix(Double factor = Double.NaN) => Factor = Double.IsNaN(factor) ? TPrefix.ToSi(1d) : factor;
    public Double ToSi(Double value) => TPrefix.ToSi(in value);
    public Double FromSi(Double value) => TPrefix.FromSi(in value);

    public override String ToString() => TPrefix.Representation;
}
