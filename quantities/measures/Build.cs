namespace Quantities.Measures;

internal static class Build<TMeasure> where TMeasure : IMeasure
{
    private static readonly Map defaultMap = new() {
        Injector = new Default(),
        ToSi = TMeasure.ToSi,
        FromSi = TMeasure.FromSi,
        Representation = TMeasure.Representation
    };
    public static Quant With(in Double value) => new(value, in defaultMap);
    public static Quant With<TInjector>(in Double value)
        where TInjector : IInjector, new()
    {
        return new(value, in MapPool<TInjector>.Item);
    }
    public static Quant With(in Quant value)
    {
        Double projection = TMeasure.FromSi(value.ToSi());
        return new(in projection, in defaultMap);
    }
    public static Quant With<TInjector>(in Quant value)
        where TInjector : IInjector, new()
    {
        Double projection = TMeasure.FromSi(value.ToSi());
        return new(in projection, in MapPool<TInjector>.Item);
    }

    private static class MapPool<TInjector>
        where TInjector : IInjector, new()
    {
        public static readonly Map Item = defaultMap.With(new TInjector());
    }

    private sealed class Default : IInjector
    {
        public T Inject<T>(in ICreate<T> creator, in Double value)
        {
            return creator.Create<TMeasure>(in value);
        }
    }
}