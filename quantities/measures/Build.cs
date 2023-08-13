using Quantities.Numerics;

namespace Quantities.Measures;

internal static class Build<TMeasure> where TMeasure : IMeasure
{
    private static readonly Map defaultMap = new(Polynomial.Of<TMeasure>()) {
        Injector = new Linear<TMeasure>(),
        Serialize = TMeasure.Write,
        Representation = TMeasure.Representation
    };
    public static Quant With(in Double value) => new(in value, in defaultMap);
    public static Quant With<TInjector>(in Double value)
        where TInjector : IInjector, new() => new(in value, in MapPool<TInjector>.Item);
    public static Quant With(in Quant value) => value.Project(in defaultMap);
    public static Quant With<TInjector>(in Quant value)
        where TInjector : IInjector, new() => value.Project(in MapPool<TInjector>.Item);

    private static class MapPool<TInjector>
        where TInjector : IInjector, new()
    {
        public static readonly Map Item = defaultMap.With(new TInjector());
    }
}