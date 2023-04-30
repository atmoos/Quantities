using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities.Quantities.Roots;

internal sealed class FractionalRoot<TNominator, TDenominator> : IRoot
    where TNominator : ISiUnit
    where TDenominator : ISiUnit
{
    public static Quant One => throw new NotImplementedException();
    public static Quant Zero => throw new NotImplementedException();
    Quant IPrefixInject<Quant>.Identity(in Double value) => value.AsFraction<Si<TNominator>, Si<TDenominator>>();
    Quant IPrefixInject<Quant>.Inject<TPrefix>(in Double value) => value.AsFraction<Si<TPrefix, TNominator>, Si<TDenominator>>();
}
