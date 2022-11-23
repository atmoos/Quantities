using Quantities.Dimensions;
using Quantities.Units.Other;

namespace Quantities;

// ToDo: Consider renaming to "Some"
internal interface IOther<out TSelf, in TDimension>
    where TSelf : TDimension
    where TDimension : IDimension
{
    public TSelf ToOther<TUnit>()
        where TUnit : IOther, TDimension;
    public static abstract TSelf Other<TUnit>(in Double value)
        where TUnit : IOther, TDimension;
}
