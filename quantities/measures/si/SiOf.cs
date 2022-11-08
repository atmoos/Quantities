using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Unit.Si;

namespace Quantities.Measures.Si;

internal readonly struct Si<TPrefix, TUnit> : ISi, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit
{
    private static readonly Double scaling = Math.Pow(10, TPrefix.Exponent + TUnit.Offset);
    public static Double Normalise(in Double value) => scaling * value;
    public static Double Renormalise(in Double value) => value / scaling;
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
}

internal readonly struct SiOf<TDim, TSiMeasure> : ISi<TDim, TSiMeasure>
    where TDim : IDimension
    where TSiMeasure : ISi, ILinear
{
    private static readonly Double scaling = Math.Pow(TSiMeasure.Normalise(1d), TDim.Exponent);
    public static Double Normalise(in Double value) => scaling * value;
    public static Double Renormalise(in Double value) => value / scaling;
    public static String Representation { get; } = $"{TSiMeasure.Representation}{TDim.Representation}";
}
