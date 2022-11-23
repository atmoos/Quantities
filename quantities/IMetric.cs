using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities;

internal interface IMetric<out TSelf, in TDimension>
    where TSelf : TDimension
    where TDimension : IDimension
{
    public TSelf ToMetric<TUnit>()
        where TUnit : ISiAcceptedUnit, TDimension;
    public TSelf ToMetric<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiAcceptedUnit, TDimension;
    public static abstract TSelf Metric<TUnit>(in Double value)
        where TUnit : ISiAcceptedUnit, TDimension;
    public static abstract TSelf Metric<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiAcceptedUnit, TDimension;
}
