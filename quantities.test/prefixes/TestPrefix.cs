using Quantities.Numerics;

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
    private static readonly Polynomial prefix = Polynomial.Of<TPrefix>();
    public Double Factor { get; }
    public TestPrefix(Double factor = Double.NaN) => Factor = Double.IsNaN(factor) ? prefix.Evaluate(1d) : factor;
    public Double ToSi(Double value) => prefix.Evaluate(in value);
    public Double FromSi(Double value) => prefix.Inverse(in value);

    public override String ToString() => TPrefix.Representation;
}
