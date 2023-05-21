using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities;

public static class Extensions
{
    internal static Quant To<TDim, TMeasure>(this Double value)
      where TDim : Measures.IDimension where TMeasure : IMeasure
      => Build<Power<TDim, TMeasure>>.With<Linear<TMeasure>>(in value);
    internal static Quant To<TDim, TMeasure>(this Quant value)
      where TDim : Measures.IDimension where TMeasure : IMeasure
      => Build<Power<TDim, TMeasure>>.With<Linear<TMeasure>>(in value);
    internal static Quant As<TMeasure>(this Double value)
      where TMeasure : IMeasure
      => Build<TMeasure>.With(in value);
    internal static Quant As<TMeasure>(this Quant value)
      where TMeasure : IMeasure
      => Build<TMeasure>.With(in value);
    internal static Quant As<TMeasure, TAlias>(this Double value)
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new()
      => Build<TMeasure>.With<TAlias>(in value);
    internal static Quant As<TMeasure, TAlias>(this Quant value)
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new()
      => Build<TMeasure>.With<TAlias>(in value);
    internal static Quant AsFraction<TLeft, TRight>(this Double value)
      where TLeft : IMeasure, ILinear
      where TRight : IMeasure, ILinear
      => Build<Fraction<TLeft, TRight>>.With(in value);
}