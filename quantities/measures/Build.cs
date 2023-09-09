namespace Quantities.Measures;

internal static class Build<TMeasure> where TMeasure : IMeasure
{
    private static readonly Measure defaultMeasure = new(TMeasure.Poly) {
        Injector = new Linear<TMeasure>(),
        Serialize = TMeasure.Write,
        Representation = TMeasure.Representation
    };
    public static Quantity With(in Double value) => new(in value, in defaultMeasure);
    public static Quantity With<TInjector>(in Double value)
        where TInjector : IInjector, new() => new(in value, in InjectPool<TInjector>.Item);
    public static Quantity With(in Quantity value) => value.Project(in defaultMeasure);
    public static Quantity With<TInjector>(in Quantity value)
        where TInjector : IInjector, new() => value.Project(in InjectPool<TInjector>.Item);

    private static class InjectPool<TInjector>
        where TInjector : IInjector, new()
    {
        public static readonly Measure Item = defaultMeasure.With(new TInjector());
    }
}
