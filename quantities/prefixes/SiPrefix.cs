using static System.Math;

namespace Quantities.Prefixes;

internal static class SiPrefix
{
    public static T Scale<T>(in Double value, IPrefixInject<T> injector)
    {
        // This is faster than taking the logarithm!
        return Abs(value) switch {
            var yotta when yotta >= 1e24 => Scale<Yotta>(in injector, in value),
            var zetta when zetta >= 1e21 => Scale<Zetta>(in injector, in value),
            var exa when exa >= 1e18 => Scale<Exa>(in injector, in value),
            var peta when peta >= 1e15 => Scale<Peta>(in injector, in value),
            var tera when tera >= 1e12 => Scale<Tera>(in injector, in value),
            var giga when giga >= 1e9 => Scale<Giga>(in injector, in value),
            var mega when mega >= 1e6 => Scale<Mega>(in injector, in value),
            var kilo when kilo >= 1e3 => Scale<Kilo>(in injector, in value),
            var hecto when hecto >= 1e2 => Scale<Hecto>(in injector, in value),
            var deca when deca >= 1e1 => Scale<Deca>(in injector, in value),
            var identity when identity >= 1e0 => injector.Identity(in value),
            var deci when deci >= 1e-1 => Scale<Deci>(in injector, in value),
            var centi when centi >= 1e-2 => Scale<Centi>(in injector, in value),
            var milli when milli >= 1e-3 => Scale<Milli>(in injector, in value),
            var micro when micro >= 1e-6 => Scale<Micro>(in injector, in value),
            var nano when nano >= 1e-9 => Scale<Nano>(in injector, in value),
            var pico when pico >= 1e-12 => Scale<Pico>(in injector, in value),
            var femto when femto >= 1e-15 => Scale<Femto>(in injector, in value),
            var atto when atto >= 1e-18 => Scale<Atto>(in injector, in value),
            var zepto when zepto >= 1e-21 => Scale<Zepto>(in injector, in value),
            _ => Scale<Yocto>(in injector, in value)
        };

        static T Scale<TPrefix>(in IPrefixInject<T> injector, in Double value)
            where TPrefix : IPrefix
        {
            return injector.Inject<TPrefix>(TPrefix.FromSi(in value));
        }
    }
    public static T ScaleThree<T>(in Double value, IPrefixInject<T> injector)
    {
        var fractionalExponent = Log10(value);
        var rounding = fractionalExponent >= 0d ? MidpointRounding.ToZero : MidpointRounding.ToNegativeInfinity;
        return (Int32)Round(fractionalExponent, rounding) switch {
            Zetta.Exp + 2 => injector.Inject<Zetta>(value / 1e21),
            Zetta.Exp + 1 => injector.Inject<Zetta>(value / 1e21),
            Zetta.Exp => injector.Inject<Zetta>(value / 1e21),
            Exa.Exp + 2 => injector.Inject<Exa>(value / 1e18),
            Exa.Exp + 1 => injector.Inject<Exa>(value / 1e18),
            Exa.Exp => injector.Inject<Exa>(value / 1e18),
            Peta.Exp + 2 => injector.Inject<Peta>(value / 1e15),
            Peta.Exp + 1 => injector.Inject<Peta>(value / 1e15),
            Peta.Exp => injector.Inject<Peta>(value / 1e15),
            Tera.Exp + 2 => injector.Inject<Tera>(value / 1e12),
            Tera.Exp + 1 => injector.Inject<Tera>(value / 1e12),
            Tera.Exp => injector.Inject<Tera>(value / 1e12),
            Giga.Exp + 2 => injector.Inject<Giga>(value / 1e9),
            Giga.Exp + 1 => injector.Inject<Giga>(value / 1e9),
            Giga.Exp => injector.Inject<Giga>(value / 1e9),
            Mega.Exp + 2 => injector.Inject<Mega>(value / 1e6),
            Mega.Exp + 1 => injector.Inject<Mega>(value / 1e6),
            Mega.Exp => injector.Inject<Mega>(value / 1e6),
            Kilo.Exp + 2 => injector.Inject<Kilo>(value / 1e3),
            Kilo.Exp + 1 => injector.Inject<Kilo>(value / 1e3),
            Kilo.Exp => injector.Inject<Kilo>(value / 1e3),
            Hecto.Exp => injector.Identity(in value),
            Deca.Exp => injector.Identity(in value),
            UnitPrefix.Exp => injector.Identity(in value),
            Deci.Exp => injector.Inject<Milli>(1e3 * value),
            Centi.Exp => injector.Inject<Milli>(1e3 * value),
            Milli.Exp => injector.Inject<Milli>(1e3 * value),
            Micro.Exp + 2 => injector.Inject<Micro>(1e6 * value),
            Micro.Exp + 1 => injector.Inject<Micro>(1e6 * value),
            Micro.Exp => injector.Inject<Micro>(1e6 * value),
            Nano.Exp + 2 => injector.Inject<Nano>(1e9 * value),
            Nano.Exp + 1 => injector.Inject<Nano>(1e9 * value),
            Nano.Exp => injector.Inject<Nano>(1e9 * value),
            Pico.Exp + 2 => injector.Inject<Pico>(1e12 * value),
            Pico.Exp + 1 => injector.Inject<Pico>(1e12 * value),
            Pico.Exp => injector.Inject<Pico>(1e12 * value),
            Femto.Exp + 2 => injector.Inject<Femto>(1e15 * value),
            Femto.Exp + 1 => injector.Inject<Femto>(1e15 * value),
            Femto.Exp => injector.Inject<Femto>(1e15 * value),
            Atto.Exp + 2 => injector.Inject<Atto>(1e18 * value),
            Atto.Exp + 1 => injector.Inject<Atto>(1e18 * value),
            Atto.Exp => injector.Inject<Atto>(1e18 * value),
            Zepto.Exp + 2 => injector.Inject<Zepto>(1e21 * value),
            Zepto.Exp + 1 => injector.Inject<Zepto>(1e21 * value),
            Zepto.Exp => injector.Inject<Zepto>(1e21 * value),
            Yocto.Exp + 2 => injector.Inject<Yocto>(1e24 * value),
            Yocto.Exp + 1 => injector.Inject<Yocto>(1e24 * value),
            var large when large >= Yotta.Exp => injector.Inject<Yotta>(value / 1e24),
            _ => injector.Inject<Yocto>(1e24 * value)
        };
    }
}
