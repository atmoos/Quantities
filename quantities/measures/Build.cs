namespace Quantities.Measures;

internal static class Build<TMeasure> where TMeasure : IMeasure
{
    private static readonly Map defaultMap = new(TMeasure.Poly) {
        Injector = new Linear<TMeasure>(),
        Serialize = TMeasure.Write,
        Representation = TMeasure.Representation
    };
    public static Quantity With(in Double value) => new(in value, in defaultMap);
    public static Quantity With<TInjector>(in Double value)
        where TInjector : IInjector, new() => new(in value, in MapPool<TInjector>.Item);
    public static Quantity With(in Quantity value) => value.Project(in defaultMap);
    public static Quantity With<TInjector>(in Quantity value)
        where TInjector : IInjector, new() => value.Project(in MapPool<TInjector>.Item);

    private static class MapPool<TInjector>
        where TInjector : IInjector, new()
    {
        public static readonly Map Item = defaultMap.With(new TInjector());
    }
}
