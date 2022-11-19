using System.Diagnostics;

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

[DebuggerDisplay(nameof(Yotta))]
public readonly struct Yotta : IPrefix, IScaleUp
{
    internal const Int32 Exp = 24;
    static Double ITransform.ToSi(in Double value) => 1e24 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e24;
    static String IRepresentable.Representation => "Y";
}
[DebuggerDisplay(nameof(Zetta))]
public readonly struct Zetta : IPrefix, IScaleUp
{
    internal const Int32 Exp = 21;
    static Double ITransform.ToSi(in Double value) => 1e21 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e21;
    static String IRepresentable.Representation => "Z";
}
[DebuggerDisplay(nameof(Exa))]
public readonly struct Exa : IPrefix, IScaleUp
{
    internal const Int32 Exp = 18;
    static Double ITransform.ToSi(in Double value) => 1e18 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e18;
    static String IRepresentable.Representation => "E";
}
[DebuggerDisplay(nameof(Peta))]
public readonly struct Peta : IPrefix, IScaleUp
{
    internal const Int32 Exp = 15;
    static Double ITransform.ToSi(in Double value) => 1e15 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e15;
    static String IRepresentable.Representation => "P";
}
[DebuggerDisplay(nameof(Tera))]
public readonly struct Tera : IPrefix, IScaleUp
{
    internal const Int32 Exp = 12;
    static Double ITransform.ToSi(in Double value) => 1e12 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e12;
    static String IRepresentable.Representation => "T";
}
[DebuggerDisplay(nameof(Giga))]
public readonly struct Giga : IPrefix, IScaleUp
{
    internal const Int32 Exp = 9;
    static Double ITransform.ToSi(in Double value) => 1e9 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e9;
    static String IRepresentable.Representation => "G";
}
[DebuggerDisplay(nameof(Mega))]
public readonly struct Mega : IPrefix, IScaleUp
{
    internal const Int32 Exp = 6;
    static Double ITransform.ToSi(in Double value) => 1e6 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e6;
    static String IRepresentable.Representation => "M";
}
[DebuggerDisplay(nameof(Kilo))]
public readonly struct Kilo : IPrefix, IScaleUp
{
    internal const Int32 Exp = 3;
    static Double ITransform.ToSi(in Double value) => 1e3 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e3;
    static String IRepresentable.Representation => "K";
}
[DebuggerDisplay(nameof(Hecto))]
public readonly struct Hecto : IPrefix, IScaleUp
{
    internal const Int32 Exp = 2;
    static Double ITransform.ToSi(in Double value) => 1e2 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e2;
    static String IRepresentable.Representation => "h";
}
[DebuggerDisplay(nameof(Deca))]
public readonly struct Deca : IPrefix, IScaleUp
{
    internal const Int32 Exp = 1;
    static Double ITransform.ToSi(in Double value) => 1e1 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e1;
    static String IRepresentable.Representation => "da";
}
[DebuggerDisplay("1")]
internal readonly struct UnitPrefix : IPrefix, IScaleUp, IScaleDown // Since we use it by default on unit-less instantiations.
{
    internal const Int32 Exp = 0;
    static Double ITransform.ToSi(in Double value) => value;
    static Double ITransform.FromSi(in Double value) => value;
    static String IRepresentable.Representation => String.Empty;
}
[DebuggerDisplay(nameof(Deci))]
public readonly struct Deci : IPrefix, IScaleDown
{
    internal const Int32 Exp = -1;
    static Double ITransform.ToSi(in Double value) => value / 1e1;
    static Double ITransform.FromSi(in Double value) => 1e1 * value;
    static String IRepresentable.Representation => "d";
}
[DebuggerDisplay(nameof(Centi))]
public readonly struct Centi : IPrefix, IScaleDown
{
    internal const Int32 Exp = -2;
    static Double ITransform.ToSi(in Double value) => value / 1e2;
    static Double ITransform.FromSi(in Double value) => 1e2 * value;
    static String IRepresentable.Representation => "c";
}
[DebuggerDisplay(nameof(Milli))]
public readonly struct Milli : IPrefix, IScaleDown
{
    internal const Int32 Exp = -3;
    static Double ITransform.ToSi(in Double value) => value / 1e3;
    static Double ITransform.FromSi(in Double value) => 1e3 * value;
    static String IRepresentable.Representation => "m";
}
[DebuggerDisplay(nameof(Micro))]
public readonly struct Micro : IPrefix, IScaleDown
{
    internal const Int32 Exp = -6;
    static Double ITransform.ToSi(in Double value) => value / 1e6;
    static Double ITransform.FromSi(in Double value) => 1e6 * value;
    static String IRepresentable.Representation => "μ";
}
[DebuggerDisplay(nameof(Nano))]
public readonly struct Nano : IPrefix, IScaleDown
{
    internal const Int32 Exp = -9;
    static Double ITransform.ToSi(in Double value) => value / 1e9;
    static Double ITransform.FromSi(in Double value) => 1e9 * value;
    static String IRepresentable.Representation => "n";
}
[DebuggerDisplay(nameof(Pico))]
public readonly struct Pico : IPrefix, IScaleDown
{
    internal const Int32 Exp = -12;
    static Double ITransform.ToSi(in Double value) => value / 1e12;
    static Double ITransform.FromSi(in Double value) => 1e12 * value;
    static String IRepresentable.Representation => "p";
}
[DebuggerDisplay(nameof(Femto))]
public readonly struct Femto : IPrefix, IScaleDown
{
    internal const Int32 Exp = -15;
    static Double ITransform.ToSi(in Double value) => value / 1e15;
    static Double ITransform.FromSi(in Double value) => 1e15 * value;
    static String IRepresentable.Representation => "f";
}
[DebuggerDisplay(nameof(Atto))]
public readonly struct Atto : IPrefix, IScaleDown
{
    internal const Int32 Exp = -18;
    static Double ITransform.ToSi(in Double value) => value / 1e18;
    static Double ITransform.FromSi(in Double value) => 1e18 * value;
    static String IRepresentable.Representation => "a";
}
[DebuggerDisplay(nameof(Zepto))]
public readonly struct Zepto : IPrefix, IScaleDown
{
    internal const Int32 Exp = -21;
    static Double ITransform.ToSi(in Double value) => value / 1e21;
    static Double ITransform.FromSi(in Double value) => 1e21 * value;
    static String IRepresentable.Representation => "z";
}
[DebuggerDisplay(nameof(Yocto))]
public readonly struct Yocto : IPrefix, IScaleDown
{
    internal const Int32 Exp = -24;
    static Double ITransform.ToSi(in Double value) => value / 1e24;
    static Double ITransform.FromSi(in Double value) => 1e24 * value;
    static String IRepresentable.Representation => "y";
}
