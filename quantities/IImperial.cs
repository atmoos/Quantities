using Quantities.Dimensions;
using Quantities.Units.Imperial;

namespace Quantities;

internal interface IImperial<out TSelf, in TDimension>
    where TSelf : TDimension
    where TDimension : IDimension
{
    public TSelf ToImperial<TUnit>()
        where TUnit : IImperialUnit, TDimension;
    public static abstract TSelf Imperial<TUnit>(in Double value)
        where TUnit : IImperialUnit, TDimension;
}
