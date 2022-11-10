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

public interface IPrefix
{
    internal static abstract Int32 Exponent { get; }
    internal static abstract String Representation { get; }
}

public readonly struct Yotta : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 24;
    static String IPrefix.Representation => "Y";
}
public readonly struct Zetta : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 21;
    static String IPrefix.Representation => "Z";
}
public readonly struct Exa : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 18;
    static String IPrefix.Representation => "E";
}
public readonly struct Peta : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 15;
    static String IPrefix.Representation => "P";
}
public readonly struct Tera : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 12;
    static String IPrefix.Representation => "T";
}
public readonly struct Giga : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 9;
    static String IPrefix.Representation => "G";
}
public readonly struct Mega : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 6;
    static String IPrefix.Representation => "M";
}
public readonly struct Kilo : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 3;
    static String IPrefix.Representation => "K";
}
public readonly struct Hecto : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 2;
    static String IPrefix.Representation => "h";
}
public readonly struct Deca : IPrefix, IScaleUp
{
    static Int32 IPrefix.Exponent => 1;
    static String IPrefix.Representation => "da";
}
[System.Diagnostics.DebuggerDisplay("1")]
internal readonly struct UnitPrefix : IPrefix, IScaleUp, IScaleDown // Since we use it by default on unit-less instantiations.
{
    static Int32 IPrefix.Exponent => 0;
    static String IPrefix.Representation => String.Empty;
}
public readonly struct Deci : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -1;
    static String IPrefix.Representation => "d";
}
public readonly struct Centi : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -2;
    static String IPrefix.Representation => "c";
}
public readonly struct Milli : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -3;
    static String IPrefix.Representation => "m";
}
public readonly struct Micro : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -6;
    static String IPrefix.Representation => "μ";
}
public readonly struct Nano : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -9;
    static String IPrefix.Representation => "n";
}
public readonly struct Pico : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -12;
    static String IPrefix.Representation => "p";
}
public readonly struct Femto : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -15;
    static String IPrefix.Representation => "f";
}
public readonly struct Atto : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -18;
    static String IPrefix.Representation => "a";
}
public readonly struct Zepto : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -21;
    static String IPrefix.Representation => "z";
}
public readonly struct Yocto : IPrefix, IScaleDown
{
    static Int32 IPrefix.Exponent => -24;
    static String IPrefix.Representation => "y";
}
