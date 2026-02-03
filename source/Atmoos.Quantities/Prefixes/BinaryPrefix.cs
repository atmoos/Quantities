using static System.Math;

namespace Atmoos.Quantities.Prefixes;

internal static class BinaryPrefix
{
    public static T Scale<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the base 2 logarithm!
        return Abs(value) switch {
            var identity when identity < Kibi.Factor => injector.Identity(in value),
            var deca when deca < Mebi.Factor => Build<Kibi>.Scaled(in injector, in value),
            var hecto when hecto < Gibi.Factor => Build<Mebi>.Scaled(in injector, in value),
            var kilo when kilo < Tebi.Factor => Build<Gibi>.Scaled(in injector, in value),
            var mega when mega < Pebi.Factor => Build<Tebi>.Scaled(in injector, in value),
            var giga when giga < Exbi.Factor => Build<Pebi>.Scaled(in injector, in value),
            var tera when tera < Zebi.Factor => Build<Exbi>.Scaled(in injector, in value),
            var peta when peta < Yobi.Factor => Build<Zebi>.Scaled(in injector, in value),
            _ => Build<Yobi>.Scaled(in injector, in value)
        };
    }
}
