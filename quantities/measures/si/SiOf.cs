using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Unit;
using Quantities.Unit.Si;

using static System.Math;

namespace Quantities.Measures.Si;

internal readonly struct Accepted<TUnit> : ISi, ILinear
    where TUnit : IUnit, ITransform
{
    public static Double Normalise(in Double value) => TUnit.ToSi(in value);
    public static Double Renormalise(in Double value) => TUnit.FromSi(in value);
    public static T Inject<T>(in ICreateLinear<T> creator, in Double value) => creator.CreateSi<Accepted<TUnit>>(in value);
    public static String Representation => TUnit.Representation;
}

internal readonly struct Si<TPrefix, TUnit> : ISi, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit
{
    private static readonly Double normalise = Pow(10, TPrefix.Exponent + TUnit.Offset);
    public static Double Normalise(in Double value) => normalise * value;
    public static Double Renormalise(in Double value) => value / normalise;
    public static T Inject<T>(in ICreateLinear<T> creator, in Double value) => creator.CreateSi<Si<TPrefix, TUnit>>(in value);
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
}

internal readonly struct SiDivide<TNominator, TDenominator> : ISi, ILinear
    where TNominator : ISi, ILinear
    where TDenominator : ISi, ILinear
{
    private static readonly Double normalise = TDenominator.Renormalise(1d);
    private static readonly Double renormalise = TDenominator.Normalise(1d);
    public static Double Normalise(in Double value) => normalise * TNominator.Normalise(in value);
    public static Double Renormalise(in Double value) => renormalise * TNominator.Renormalise(in value);
    public static T Inject<T>(in ICreateLinear<T> creator, in Double value) => creator.CreateSi<SiDivide<TNominator, TDenominator>>(in value);
    public static String Representation { get; } = $"{TNominator.Representation}/{TDenominator.Representation}";
}

internal readonly struct SiOf<TDim, TSiMeasure> : ISi<TDim, TSiMeasure>
    where TDim : IDimension
    where TSiMeasure : ISi, ILinear
{
    private static readonly Double normalise = Pow(TSiMeasure.Normalise(1d), TDim.Exponent);
    private static readonly Double renormalise = Pow(TSiMeasure.Renormalise(1d), TDim.Exponent);
    public static Double Normalise(in Double value) => normalise * value;
    public static Double Renormalise(in Double value) => renormalise * value;
    public static T Inject<T>(in ICreateLinear<T> creator, in Double value) => creator.CreateSi<TSiMeasure>(in value);
    public static String Representation { get; } = $"{TSiMeasure.Representation}{TDim.Representation}";
}
