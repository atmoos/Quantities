using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities;

internal interface ISi<out TSelf, in TDimension>
    where TSelf : TDimension
    where TDimension : IDimension
{
    public TSelf To<TUnit>()
        where TUnit : ISiUnit, TDimension;
    public TSelf To<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension;
    public static abstract TSelf Si<TUnit>(in Double value)
        where TUnit : ISiUnit, TDimension;
    public static abstract TSelf Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TDimension;
}
