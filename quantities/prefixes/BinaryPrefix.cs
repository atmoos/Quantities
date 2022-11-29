using Quantities.Dimensions;
using Quantities.Units.Si;
using static System.Math;

namespace Quantities.Prefixes;

internal static class BinaryPrefix
{
    public static T Scale<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the base 2 logarithm!
        return Abs(value) switch {
            var identity when identity < Kibi.Factor => injector.Identity(in value),
            var deca when deca < Mebi.Factor => Scale<Kibi>(in injector, in value),
            var hecto when hecto < Gibi.Factor => Scale<Mebi>(in injector, in value),
            var kilo when kilo < Tebi.Factor => Scale<Gibi>(in injector, in value),
            var mega when mega < Pebi.Factor => Scale<Tebi>(in injector, in value),
            var giga when giga < Exbi.Factor => Scale<Pebi>(in injector, in value),
            var tera when tera < Zebi.Factor => Scale<Exbi>(in injector, in value),
            var peta when peta < Yobi.Factor => Scale<Zebi>(in injector, in value),
            _ => Scale<Yobi>(in injector, in value)
        };

        static T Scale<TPrefix>(in IPrefixInject<T> injector, in Double value)
            where TPrefix : IBinaryPrefix
        {
            return injector.Inject<TPrefix>(TPrefix.FromSi(in value));
        }
    }
}
