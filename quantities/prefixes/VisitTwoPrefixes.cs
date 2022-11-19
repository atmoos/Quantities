
namespace Quantities.Prefixes;

internal sealed class Multiply : IPrefixVisitor<Double>
{
    public (IPrefixVisitor<Double> next, Double value) Visit<TPrefix>(in Double value) where TPrefix : IPrefix
    {
        return (this, TPrefix.ToSi(in value));
    }
}

internal sealed class Division : IPrefixVisitor<Double>
{
    private static readonly Division nominator = new();
    private static readonly Denominator denominator = new();

    public (IPrefixVisitor<Double> next, Double value) Visit<TPrefix>(in Double value) where TPrefix : IPrefix
    {
        return (denominator, TPrefix.ToSi(in value));
    }

    private sealed class Denominator : IPrefixVisitor<Double>
    {
        public (IPrefixVisitor<Double> next, Double value) Visit<TPrefix>(in Double value) where TPrefix : IPrefix
        {
            return (nominator, TPrefix.FromSi(in value));
        }
    }
}
