using static System.Math;

namespace Quantities.Prefixes;

internal static class MetricPrefix
{
    public static T Scale<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the logarithm!
        return Abs(value) >= 1d ? ScaleUp(in value, injector) : ScaleDown(in value, injector);

        static T ScaleUp(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var identity when identity < 1e1 => injector.Identity(in value),
            var deca when deca < 1e2 => Build<Deca>.Scaled(in injector, in value),
            var hecto when hecto < 1e3 => Build<Hecto>.Scaled(in injector, in value),
            var kilo when kilo < 1e6 => Build<Kilo>.Scaled(in injector, in value),
            var mega when mega < 1e9 => Build<Mega>.Scaled(in injector, in value),
            var giga when giga < 1e12 => Build<Giga>.Scaled(in injector, in value),
            var tera when tera < 1e15 => Build<Tera>.Scaled(in injector, in value),
            var peta when peta < 1e18 => Build<Peta>.Scaled(in injector, in value),
            var exa when exa < 1e21 => Build<Exa>.Scaled(in injector, in value),
            var zetta when zetta < 1e24 => Build<Zetta>.Scaled(in injector, in value),
            var yotta when yotta < 1e27 => Build<Yotta>.Scaled(in injector, in value),
            var ronna when ronna < 1e30 => Build<Ronna>.Scaled(in injector, in value),
            _ => Build<Quetta>.Scaled(in injector, in value)
        };

        static T ScaleDown(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var deci when deci >= 1e-1 => Build<Deci>.Scaled(in injector, in value),
            var centi when centi >= 1e-2 => Build<Centi>.Scaled(in injector, in value),
            var milli when milli >= 1e-3 => Build<Milli>.Scaled(in injector, in value),
            var micro when micro >= 1e-6 => Build<Micro>.Scaled(in injector, in value),
            var nano when nano >= 1e-9 => Build<Nano>.Scaled(in injector, in value),
            var pico when pico >= 1e-12 => Build<Pico>.Scaled(in injector, in value),
            var femto when femto >= 1e-15 => Build<Femto>.Scaled(in injector, in value),
            var atto when atto >= 1e-18 => Build<Atto>.Scaled(in injector, in value),
            var zepto when zepto >= 1e-21 => Build<Zepto>.Scaled(in injector, in value),
            var yocto when yocto >= 1e-24 => Build<Yocto>.Scaled(in injector, in value),
            var ronto when ronto >= 1e-27 => Build<Ronto>.Scaled(in injector, in value),
            _ => Build<Quecto>.Scaled(in injector, in value)
        };
    }
    public static T ScaleTriadic<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the logarithm!
        return Abs(value) >= 1d ? ScaleUp(in value, injector) : ScaleDown(in value, injector);

        static T ScaleUp(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var identity when identity < 1e3 => injector.Identity(in value),
            var kilo when kilo < 1e6 => Build<Kilo>.Scaled(in injector, in value),
            var mega when mega < 1e9 => Build<Mega>.Scaled(in injector, in value),
            var giga when giga < 1e12 => Build<Giga>.Scaled(in injector, in value),
            var tera when tera < 1e15 => Build<Tera>.Scaled(in injector, in value),
            var peta when peta < 1e18 => Build<Peta>.Scaled(in injector, in value),
            var exa when exa < 1e21 => Build<Exa>.Scaled(in injector, in value),
            var zetta when zetta < 1e24 => Build<Zetta>.Scaled(in injector, in value),
            var yotta when yotta < 1e27 => Build<Yotta>.Scaled(in injector, in value),
            var ronna when ronna < 1e30 => Build<Ronna>.Scaled(in injector, in value),
            _ => Build<Quetta>.Scaled(in injector, in value)
        };

        static T ScaleDown(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var milli when milli >= 1e-3 => Build<Milli>.Scaled(in injector, in value),
            var micro when micro >= 1e-6 => Build<Micro>.Scaled(in injector, in value),
            var nano when nano >= 1e-9 => Build<Nano>.Scaled(in injector, in value),
            var pico when pico >= 1e-12 => Build<Pico>.Scaled(in injector, in value),
            var femto when femto >= 1e-15 => Build<Femto>.Scaled(in injector, in value),
            var atto when atto >= 1e-18 => Build<Atto>.Scaled(in injector, in value),
            var zepto when zepto >= 1e-21 => Build<Zepto>.Scaled(in injector, in value),
            var yocto when yocto >= 1e-24 => Build<Yocto>.Scaled(in injector, in value),
            var ronto when ronto >= 1e-27 => Build<Ronto>.Scaled(in injector, in value),
            _ => Build<Quecto>.Scaled(in injector, in value)
        };
    }
}
