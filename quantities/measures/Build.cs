namespace Quantities.Measures;

internal static class Build<TMeasure> where TMeasure : IMeasure
{
    private static readonly Map defaultMap = new() {
        Injector = new Linear<TMeasure>(),
        ToSi = TMeasure.ToSi,
        FromSi = TMeasure.FromSi,
        Serialize = TMeasure.Write,
        Representation = TMeasure.Representation
    };
    public static Quant With(in Double value) => new(in value, in defaultMap);
    public static Quant With<TInjector>(in Double value)
        where TInjector : IInjector, new()
    {
        return new(in value, in MapPool<TInjector>.Item);
    }
    public static Quant With(in Quant value)
    {
        // ToDo: Check reference equality of the default map and quant's map.
        //       When equal, no projection needs to be done...
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
}