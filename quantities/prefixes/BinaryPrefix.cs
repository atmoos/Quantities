using static System.Math;

namespace Quantities.Prefixes;

internal static class BinaryPrefix
{
    public static T Scale<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the base 2 logarithm!
        return Abs(value) switch {
            var identity when identity < Kibi.Factor => injector.Identity(in value),
            var deca when deca < Mebi.Factor => Inject<Kibi>.Into(in injector, in value),
            var hecto when hecto < Gibi.Factor => Inject<Mebi>.Into(in injector, in value),
            var kilo when kilo < Tebi.Factor => Inject<Gibi>.Into(in injector, in value),
            var mega when mega < Pebi.Factor => Inject<Tebi>.Into(in injector, in value),
            var giga when giga < Exbi.Factor => Inject<Pebi>.Into(in injector, in value),
            var tera when tera < Zebi.Factor => Inject<Exbi>.Into(in injector, in value),
            var peta when peta < Yobi.Factor => Inject<Zebi>.Into(in injector, in value),
            _ => Inject<Yobi>.Into(in injector, in value)
        };
    }
}
