namespace Quantities.Measures;

internal static class Build<TMeasure> where TMeasure : IMeasure
{
    private static readonly Measure defaultMeasure = Measure.Create<TMeasure>();
    public static Quantity With(in Double value) => new(in value, in defaultMeasure);
    public static Quantity With<TInjector>(in Double value)
        where TInjector : IInjector => new(in value, in InjectPool<TInjector>.Item);
    public static Quantity With(in Quantity value) => value.Project(in defaultMeasure);
    public static Quantity With<TInjector>(in Quantity value)
        where TInjector : IInjector => value.Project(in InjectPool<TInjector>.Item);

    private static class InjectPool<TInjector>
        where TInjector : IInjector
    {
        public static readonly Measure Item = Measure.Create<TMeasure, TInjector>();
    }
}
