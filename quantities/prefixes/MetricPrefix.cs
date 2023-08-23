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
            var deca when deca < 1e2 => Inject<Deca>.Into(in injector, in value),
            var hecto when hecto < 1e3 => Inject<Hecto>.Into(in injector, in value),
            var kilo when kilo < 1e6 => Inject<Kilo>.Into(in injector, in value),
            var mega when mega < 1e9 => Inject<Mega>.Into(in injector, in value),
            var giga when giga < 1e12 => Inject<Giga>.Into(in injector, in value),
            var tera when tera < 1e15 => Inject<Tera>.Into(in injector, in value),
            var peta when peta < 1e18 => Inject<Peta>.Into(in injector, in value),
            var exa when exa < 1e21 => Inject<Exa>.Into(in injector, in value),
            var zetta when zetta < 1e24 => Inject<Zetta>.Into(in injector, in value),
            var yotta when yotta < 1e27 => Inject<Yotta>.Into(in injector, in value),
            var ronna when ronna < 1e30 => Inject<Ronna>.Into(in injector, in value),
            _ => Inject<Quetta>.Into(in injector, in value)
        };

        static T ScaleDown(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var deci when deci >= 1e-1 => Inject<Deci>.Into(in injector, in value),
            var centi when centi >= 1e-2 => Inject<Centi>.Into(in injector, in value),
            var milli when milli >= 1e-3 => Inject<Milli>.Into(in injector, in value),
            var micro when micro >= 1e-6 => Inject<Micro>.Into(in injector, in value),
            var nano when nano >= 1e-9 => Inject<Nano>.Into(in injector, in value),
            var pico when pico >= 1e-12 => Inject<Pico>.Into(in injector, in value),
            var femto when femto >= 1e-15 => Inject<Femto>.Into(in injector, in value),
            var atto when atto >= 1e-18 => Inject<Atto>.Into(in injector, in value),
            var zepto when zepto >= 1e-21 => Inject<Zepto>.Into(in injector, in value),
            var yocto when yocto >= 1e-24 => Inject<Yocto>.Into(in injector, in value),
            var ronto when ronto >= 1e-27 => Inject<Ronto>.Into(in injector, in value),
            _ => Inject<Quecto>.Into(in injector, in value)
        };
    }
    public static T ScaleTriadic<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the logarithm!
        return Abs(value) >= 1d ? ScaleUp(in value, injector) : ScaleDown(in value, injector);

        static T ScaleUp(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var identity when identity < 1e3 => injector.Identity(in value),
            var kilo when kilo < 1e6 => Inject<Kilo>.Into(in injector, in value),
            var mega when mega < 1e9 => Inject<Mega>.Into(in injector, in value),
            var giga when giga < 1e12 => Inject<Giga>.Into(in injector, in value),
            var tera when tera < 1e15 => Inject<Tera>.Into(in injector, in value),
            var peta when peta < 1e18 => Inject<Peta>.Into(in injector, in value),
            var exa when exa < 1e21 => Inject<Exa>.Into(in injector, in value),
            var zetta when zetta < 1e24 => Inject<Zetta>.Into(in injector, in value),
            var yotta when yotta < 1e27 => Inject<Yotta>.Into(in injector, in value),
            var ronna when ronna < 1e30 => Inject<Ronna>.Into(in injector, in value),
            _ => Inject<Quetta>.Into(in injector, in value)
        };

        static T ScaleDown(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var milli when milli >= 1e-3 => Inject<Milli>.Into(in injector, in value),
            var micro when micro >= 1e-6 => Inject<Micro>.Into(in injector, in value),
            var nano when nano >= 1e-9 => Inject<Nano>.Into(in injector, in value),
            var pico when pico >= 1e-12 => Inject<Pico>.Into(in injector, in value),
            var femto when femto >= 1e-15 => Inject<Femto>.Into(in injector, in value),
            var atto when atto >= 1e-18 => Inject<Atto>.Into(in injector, in value),
            var zepto when zepto >= 1e-21 => Inject<Zepto>.Into(in injector, in value),
            var yocto when yocto >= 1e-24 => Inject<Yocto>.Into(in injector, in value),
            var ronto when ronto >= 1e-27 => Inject<Ronto>.Into(in injector, in value),
            _ => Inject<Quecto>.Into(in injector, in value)
        };
    }
}
