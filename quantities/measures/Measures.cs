using Quantities.Dimensions;
using Quantities.Measures.Transformations;
using Quantities.Prefixes;
using Quantities.Unit;
using Quantities.Unit.Si;

using static System.Math;

namespace Quantities.Measures;

internal readonly struct Si<TPrefix, TUnit> : IMeasure, ILinear
    where TPrefix : IPrefix
    where TUnit : ISiUnit, ITransform
{
    public static Double ToSi(in Double value) => TPrefix.ToSi(TUnit.ToSi(in value));
    public static Double FromSi(in Double value) => TPrefix.FromSi(TUnit.FromSi(in value));
    public static T Inject<T>(in ICreate<T> creator, in Double value) => creator.Create<Si<TPrefix, TUnit>>(in value);
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
}

internal readonly struct Other<TUnit> : IMeasure, ILinear
    where TUnit : ITransform, IRepresentable
{
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static T Inject<T>(in ICreate<T> creator, in Double value) => creator.Create<Other<TUnit>>(in value);
    public static String Representation => TUnit.Representation;
}
internal readonly struct Alias<TUnit, TAlias> : IMeasure, ILinear
    where TUnit : ITransform, IRepresentable, IUnitInject<TAlias>
    where TAlias : Dimensions.IDimension
{
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static T Inject<T>(in ICreate<T> creator, in Double value) => TUnit.Inject(new InjectAlias<TAlias, T>(creator), in value);
    public static String Representation => TUnit.Representation;
}
internal readonly struct Alias<TPrefix, TUnit, TAlias> : IMeasure, ILinear
    where TPrefix : IPrefix
    where TUnit : ITransform, IRepresentable, IUnitInject<TAlias>
    where TAlias : Dimensions.IDimension
{
    public static Double ToSi(in Double value) => TPrefix.ToSi(TUnit.ToSi(in value));
    public static Double FromSi(in Double value) => TPrefix.FromSi(TUnit.FromSi(in value));
    public static T Inject<T>(in ICreate<T> creator, in Double value) => TUnit.Inject(new InjectAlias<TAlias, T>(creator), TPrefix.ToSi(in value));
    public static String Representation { get; } = $"{TPrefix.Representation}{TUnit.Representation}";
}
internal readonly struct Divide<TNominator, TDenominator> : IMeasure
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    // the denominator scales inverted, hence the apparent inversion of toSi <-> fromSi
    private static readonly Double toSi = TDenominator.FromSi(1d);
    private static readonly Double fromSi = TDenominator.ToSi(1d);
    public static Double ToSi(in Double value) => toSi * TNominator.ToSi(in value);
    public static Double FromSi(in Double value) => fromSi * TNominator.FromSi(in value);
    public static T Inject<T>(in ICreate<T> creator, in Double value) => creator.Create<Divide<TNominator, TDenominator>>(in value);
    public static String Representation { get; } = $"{TNominator.Representation}/{TDenominator.Representation}";
}

internal readonly struct Power<TDim, TMeasure> : IMeasure
    where TDim : IDimension
    where TMeasure : IMeasure
{
    private static readonly Double normalise = Pow(TMeasure.ToSi(1d), TDim.Exponent);
    private static readonly Double renormalise = Pow(TMeasure.FromSi(1d), TDim.Exponent);
    public static Double ToSi(in Double value) => normalise * value;
    public static Double FromSi(in Double value) => renormalise * value;
    public static T Inject<T>(in ICreate<T> creator, in Double value) => creator.Create<TMeasure>(in value);
    public static String Representation { get; } = $"{TMeasure.Representation}{TDim.Representation}";
}
