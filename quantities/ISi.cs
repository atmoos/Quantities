using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Units.Si;

namespace Quantities;

internal interface ISi<out TSelf, in TDimension>
    where TSelf : TDimension
    where TDimension : IDimension
{
    public TSelf To<TUnit>()
        where TUnit : ISiBaseUnit, TDimension;
    public TSelf To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiBaseUnit, TDimension;
    public static abstract TSelf Si<TUnit>(in Double value)
        where TUnit : ISiBaseUnit, TDimension;
    public static abstract TSelf Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiBaseUnit, TDimension;
}

internal interface ISiDerived<out TSelf, in TDimension>
    where TSelf : TDimension
    where TDimension : IDimension
{
    public TSelf To<TUnit>()
        where TUnit : ISiDerivedUnit, TDimension;
    public TSelf To<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiDerivedUnit, TDimension;
    public static abstract TSelf Si<TUnit>(in Double value)
        where TUnit : ISiDerivedUnit, TDimension;
    public static abstract TSelf Si<TPrefix, TUnit>(in Double value)
        where TPrefix : IPrefix
        where TUnit : ISiDerivedUnit, TDimension;
}
