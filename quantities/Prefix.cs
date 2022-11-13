namespace Quantities.Prefixes;

/*
yotta   Y   1000000000000000000000000   10+24
zetta   Z   1000000000000000000000   10+21
exa     E   1000000000000000000   10+18
peta    P   1000000000000000   10+15
tera    T   1000000000000   10+12
giga    G   1000000000   10+9
mega    M   1000000   10+6
kilo    k   1000   10+3
hecto   h   100   10+2
deca    da  10   10+1
(none)  (none)  1   10+0
deci    d   0.1   10−1
centi   c   0.01   10−2
milli   m   0.001   10−3
micro   μ   0.000001   10−6
nano    n   0.000000001   10−9
pico    p   0.000000000001   10−12
femto   f   0.000000000000001   10−15
atto    a   0.000000000000000001   10−18
zepto   z   0.000000000000000000001   10−21
yocto   y   0.000000000000000000000001   10−24
*/

public interface IPrefix : ITransform, IRepresentable
{
}

public readonly struct Yotta : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e24 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e24;
    static String IRepresentable.Representation => "Y";
}
public readonly struct Zetta : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e21 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e21;
    static String IRepresentable.Representation => "Z";
}
public readonly struct Exa : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e18 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e18;
    static String IRepresentable.Representation => "E";
}
public readonly struct Peta : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e15 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e15;
    static String IRepresentable.Representation => "P";
}
public readonly struct Tera : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e12 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e12;
    static String IRepresentable.Representation => "T";
}
public readonly struct Giga : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e9 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e9;
    static String IRepresentable.Representation => "G";
}
public readonly struct Mega : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e6 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e6;
    static String IRepresentable.Representation => "M";
}
public readonly struct Kilo : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e3 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e3;
    static String IRepresentable.Representation => "K";
}
public readonly struct Hecto : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e2 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e2;
    static String IRepresentable.Representation => "h";
}
public readonly struct Deca : IPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e1 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e1;
    static String IRepresentable.Representation => "da";
}
[System.Diagnostics.DebuggerDisplay("1")]
internal readonly struct UnitPrefix : IPrefix, IScaleUp, IScaleDown // Since we use it by default on unit-less instantiations.
{
    static Double ITransform.ToSi(in Double value) => value;
    static Double ITransform.FromSi(in Double value) => value;
    static String IRepresentable.Representation => String.Empty;
}
public readonly struct Deci : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e1;
    static Double ITransform.FromSi(in Double value) => 1e1 * value;
    static String IRepresentable.Representation => "d";
}
public readonly struct Centi : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e2;
    static Double ITransform.FromSi(in Double value) => 1e2 * value;
    static String IRepresentable.Representation => "c";
}
public readonly struct Milli : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e3;
    static Double ITransform.FromSi(in Double value) => 1e3 * value;
    static String IRepresentable.Representation => "m";
}
public readonly struct Micro : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e6;
    static Double ITransform.FromSi(in Double value) => 1e6 * value;
    static String IRepresentable.Representation => "μ";
}
public readonly struct Nano : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e9;
    static Double ITransform.FromSi(in Double value) => 1e9 * value;
    static String IRepresentable.Representation => "n";
}
public readonly struct Pico : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e12;
    static Double ITransform.FromSi(in Double value) => 1e12 * value;
    static String IRepresentable.Representation => "p";
}
public readonly struct Femto : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e15;
    static Double ITransform.FromSi(in Double value) => 1e15 * value;
    static String IRepresentable.Representation => "f";
}
public readonly struct Atto : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e18;
    static Double ITransform.FromSi(in Double value) => 1e18 * value;
    static String IRepresentable.Representation => "a";
}
public readonly struct Zepto : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e21;
    static Double ITransform.FromSi(in Double value) => 1e21 * value;
    static String IRepresentable.Representation => "z";
}
public readonly struct Yocto : IPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e24;
    static Double ITransform.FromSi(in Double value) => 1e24 * value;
    static String IRepresentable.Representation => "y";
}
