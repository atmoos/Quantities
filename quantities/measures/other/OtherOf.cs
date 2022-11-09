namespace Quantities.Measures.Other;

internal readonly struct Other<TUnit> : IOther, Dimensions.ILinear
    where TUnit : ITransform, IRepresentable
{
    public static Double ToSi(in Double value) => TUnit.ToSi(in value);
    public static Double FromSi(in Double value) => TUnit.FromSi(in value);
    public static T Inject<T>(in ICreateLinear<T> creator, in Double value) => creator.CreateOther<Other<TUnit>>(in value);
    public static String Representation => TUnit.Representation;
}

internal readonly struct OtherOf<TDim, TImperial> : IOther
    where TDim : IDimension
    where TImperial : IOther, Dimensions.ILinear
{
    private static readonly Double toSi = Math.Pow(TImperial.ToSi(1d), TDim.Exponent);
    private static readonly Double fromSi = Math.Pow(TImperial.FromSi(1d), TDim.Exponent);
    public static Double ToSi(in Double value) => toSi * value;
    public static Double FromSi(in Double value) => fromSi * value;
    public static T Inject<T>(in ICreateLinear<T> creator, in Double value) => creator.CreateOther<TImperial>(in value);
    public static String Representation { get; } = $"{TImperial.Representation}{TDim.Representation}";
}
