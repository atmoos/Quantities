namespace Quantities.Measures.Imperial;
internal readonly struct ImperialOf<TDim, TImperial> : ITransform, IRepresentable
where TDim : IDimension
where TImperial : Dimensions.IDimension, ITransform, IRepresentable, Dimensions.ILinear
{
    private static readonly Double scaling = Math.Pow(TImperial.ToSi(1d), TDim.Exponent);
    public static Double ToSi(in Double value) => scaling * value;
    public static Double FromSi(in Double value) => value / scaling;
    public static String Representation { get; } = $"{TImperial.Representation}{TDim.Representation}";
}
