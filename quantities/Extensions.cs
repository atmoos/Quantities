using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities;

public static class Extensions
{
    internal static Quant To<TDim, TMeasure>(this in Double value)
      where TDim : Measures.IDimension where TMeasure : IMeasure
      => Build<Power<TDim, TMeasure>>.With<Linear<TMeasure>>(in value);
    internal static Quant As<TDim, TMeasure>(this in Quant value)
      where TDim : Measures.IDimension where TMeasure : IMeasure
      => Build<Power<TDim, TMeasure>>.With<Linear<TMeasure>>(in value);
    internal static Quant To<TMeasure>(this in Double value)
      where TMeasure : IMeasure
      => Build<TMeasure>.With(in value);
    internal static Quant As<TMeasure>(this in Quant value)
      where TMeasure : IMeasure
      => Build<TMeasure>.With(in value);
    internal static Quant Alias<TMeasure, TAlias>(this in Double value)
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new()
      => Build<TMeasure>.With<TAlias>(in value);
    internal static Quant Alias<TMeasure, TAlias>(this in Quant value)
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new()
      => Build<TMeasure>.With<TAlias>(in value);
}