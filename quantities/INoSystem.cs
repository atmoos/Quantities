using Quantities.Dimensions;
using Quantities.Units.NonStandard;

namespace Quantities;

internal interface INoSystem<out TSelf, in TDimension>
    where TSelf : TDimension
    where TDimension : IDimension
{
    public TSelf ToNonStandard<TUnit>()
        where TUnit : INoSystem, TDimension;
    public static abstract TSelf NonStandard<TUnit>(in Double value)
        where TUnit : INoSystem, TDimension;
}
