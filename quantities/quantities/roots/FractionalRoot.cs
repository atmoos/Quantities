using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities.Roots;

internal sealed class FractionalRoot<TNominator, TDenominator> : IRoot
    where TNominator : ISiUnit
    where TDenominator : ISiUnit
{
    public static Quantity One { get; } = 1d.To<Quotient<Si<TNominator>, Si<TDenominator>>>();
    public static Quantity Zero { get; } = 0d.To<Quotient<Si<TNominator>, Si<TDenominator>>>();
    Quantity IPrefixInject<Quantity>.Identity(in Double value) => value.To<Quotient<Si<TNominator>, Si<TDenominator>>>();
    Quantity IPrefixInject<Quantity>.Inject<TPrefix>(in Double value) => value.To<Quotient<Si<TPrefix, TNominator>, Si<TDenominator>>>();
}
