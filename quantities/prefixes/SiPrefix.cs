using static System.Math;

namespace Quantities.Prefixes;

internal static class SiPrefix
{
    public static T Scale<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the logarithm!
        return Abs(value) >= 1d ? ScaleUp(in value, injector) : ScaleDown(in value, injector);

        static T ScaleUp(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var identity when identity < 1e1 => injector.Identity(in value),
            var deca when deca < 1e2 => Scale<Deca>(in injector, in value),
            var hecto when hecto < 1e3 => Scale<Hecto>(in injector, in value),
            var kilo when kilo < 1e6 => Scale<Kilo>(in injector, in value),
            var mega when mega < 1e9 => Scale<Mega>(in injector, in value),
            var giga when giga < 1e12 => Scale<Giga>(in injector, in value),
            var tera when tera < 1e15 => Scale<Tera>(in injector, in value),
            var peta when peta < 1e18 => Scale<Peta>(in injector, in value),
            var exa when exa < 1e21 => Scale<Exa>(in injector, in value),
            var zetta when zetta < 1e24 => Scale<Zetta>(in injector, in value),
            var yotta when yotta < 1e27 => Scale<Yotta>(in injector, in value),
            var ronna when ronna < 1e30 => Scale<Ronna>(in injector, in value),
            _ => Scale<Quetta>(in injector, in value)
        };

        static T ScaleDown(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var deci when deci >= 1e-1 => Scale<Deci>(in injector, in value),
            var centi when centi >= 1e-2 => Scale<Centi>(in injector, in value),
            var milli when milli >= 1e-3 => Scale<Milli>(in injector, in value),
            var micro when micro >= 1e-6 => Scale<Micro>(in injector, in value),
            var nano when nano >= 1e-9 => Scale<Nano>(in injector, in value),
            var pico when pico >= 1e-12 => Scale<Pico>(in injector, in value),
            var femto when femto >= 1e-15 => Scale<Femto>(in injector, in value),
            var atto when atto >= 1e-18 => Scale<Atto>(in injector, in value),
            var zepto when zepto >= 1e-21 => Scale<Zepto>(in injector, in value),
            var yocto when yocto >= 1e-24 => Scale<Yocto>(in injector, in value),
            var ronto when ronto >= 1e-27 => Scale<Ronto>(in injector, in value),
            _ => Scale<Quecto>(in injector, in value)
        };

        static T Scale<TPrefix>(in IPrefixInject<T> injector, in Double value)
            where TPrefix : ISiPrefix
        {
            return injector.Inject<TPrefix>(TPrefix.FromSi(in value));
        }
    }
    public static T ScaleThree<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the logarithm!
        return Abs(value) >= 1d ? ScaleUp(in value, injector) : ScaleDown(in value, injector);

        static T ScaleUp(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var identity when identity < 1e3 => injector.Identity(in value),
            var kilo when kilo < 1e6 => Scale<Kilo>(in injector, in value),
            var mega when mega < 1e9 => Scale<Mega>(in injector, in value),
            var giga when giga < 1e12 => Scale<Giga>(in injector, in value),
            var tera when tera < 1e15 => Scale<Tera>(in injector, in value),
            var peta when peta < 1e18 => Scale<Peta>(in injector, in value),
            var exa when exa < 1e21 => Scale<Exa>(in injector, in value),
            var zetta when zetta < 1e24 => Scale<Zetta>(in injector, in value),
            var yotta when yotta < 1e27 => Scale<Yotta>(in injector, in value),
            var ronna when ronna < 1e30 => Scale<Ronna>(in injector, in value),
            _ => Scale<Quetta>(in injector, in value)
        };

        static T ScaleDown(in Double value, IPrefixInject<T> injector) => Abs(value) switch {
            var milli when milli >= 1e-3 => Scale<Milli>(in injector, in value),
            var micro when micro >= 1e-6 => Scale<Micro>(in injector, in value),
            var nano when nano >= 1e-9 => Scale<Nano>(in injector, in value),
            var pico when pico >= 1e-12 => Scale<Pico>(in injector, in value),
            var femto when femto >= 1e-15 => Scale<Femto>(in injector, in value),
            var atto when atto >= 1e-18 => Scale<Atto>(in injector, in value),
            var zepto when zepto >= 1e-21 => Scale<Zepto>(in injector, in value),
            var yocto when yocto >= 1e-24 => Scale<Yocto>(in injector, in value),
            var ronto when ronto >= 1e-27 => Scale<Ronto>(in injector, in value),
            _ => Scale<Quecto>(in injector, in value)
        };

        static T Scale<TPrefix>(in IPrefixInject<T> injector, in Double value)
            where TPrefix : ISiPrefix
        {
            return injector.Inject<TPrefix>(TPrefix.FromSi(in value));
        }
    }
}
