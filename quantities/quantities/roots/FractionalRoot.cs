using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities.Roots;

internal sealed class FractionalRoot<TNominator, TDenominator> : IRoot
    where TNominator : ISiUnit
    where TDenominator : ISiUnit
{
    public static Quant One { get; } = 1d.To<Quotient<Si<TNominator>, Si<TDenominator>>>();
    public static Quant Zero { get; } = 0d.To<Quotient<Si<TNominator>, Si<TDenominator>>>();
    Quant IPrefixInject<Quant>.Identity(in Double value) => value.To<Quotient<Si<TNominator>, Si<TDenominator>>>();
    Quant IPrefixInject<Quant>.Inject<TPrefix>(in Double value) => value.To<Quotient<Si<TPrefix, TNominator>, Si<TDenominator>>>();
}