using System.Diagnostics;

namespace Quantities.Prefixes;

/*
quetta 	Q   1000000000000000000000000000000   1e30
ronna 	R   1000000000000000000000000000   1e27
yotta   Y   1000000000000000000000000   1e24
zetta   Z   1000000000000000000000   1e21
exa     E   1000000000000000000   1e18
peta    P   1000000000000000   1e15
tera    T   1000000000000   1e12
giga    G   1000000000   1e9
mega    M   1000000   1e6
kilo    k   1000   1e3
hecto   h   100   1e2
deca    da  10   1e1
(none)  (none)  1   1e0
deci    d   0.1   1e-1
centi   c   0.01   1e-2
milli   m   0.001   1e-3
micro   μ   0.000001   1e-6
nano    n   0.000000001   1e-9
pico    p   0.000000000001   1e-12
femto   f   0.000000000000001   1e-15
atto    a   0.000000000000000001   1e-18
zepto   z   0.000000000000000000001   1e-21
yocto   y   0.000000000000000000000001   1e-24
ronto 	r 	0.000000000000000000000000001   1e-27
quecto 	q 	0.000000000000000000000000000001   1e-30
*/

[DebuggerDisplay(nameof(Quetta))]
public readonly struct Quetta : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e30 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e30;
    static String IRepresentable.Representation => "Q";
}
[DebuggerDisplay(nameof(Ronna))]
public readonly struct Ronna : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e27 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e27;
    static String IRepresentable.Representation => "R";
}
[DebuggerDisplay(nameof(Yotta))]
public readonly struct Yotta : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e24 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e24;
    static String IRepresentable.Representation => "Y";
}
[DebuggerDisplay(nameof(Zetta))]
public readonly struct Zetta : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e21 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e21;
    static String IRepresentable.Representation => "Z";
}
[DebuggerDisplay(nameof(Exa))]
public readonly struct Exa : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e18 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e18;
    static String IRepresentable.Representation => "E";
}
[DebuggerDisplay(nameof(Peta))]
public readonly struct Peta : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e15 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e15;
    static String IRepresentable.Representation => "P";
}
[DebuggerDisplay(nameof(Tera))]
public readonly struct Tera : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e12 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e12;
    static String IRepresentable.Representation => "T";
}
[DebuggerDisplay(nameof(Giga))]
public readonly struct Giga : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e9 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e9;
    static String IRepresentable.Representation => "G";
}
[DebuggerDisplay(nameof(Mega))]
public readonly struct Mega : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e6 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e6;
    static String IRepresentable.Representation => "M";
}
[DebuggerDisplay(nameof(Kilo))]
public readonly struct Kilo : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e3 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e3;
    static String IRepresentable.Representation => "K";
}
[DebuggerDisplay(nameof(Hecto))]
public readonly struct Hecto : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e2 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e2;
    static String IRepresentable.Representation => "h";
}
[DebuggerDisplay(nameof(Deca))]
public readonly struct Deca : ISiPrefix, IScaleUp
{
    static Double ITransform.ToSi(in Double value) => 1e1 * value;
    static Double ITransform.FromSi(in Double value) => value / 1e1;
    static String IRepresentable.Representation => "da";
}
[DebuggerDisplay(nameof(Deci))]
public readonly struct Deci : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e1;
    static Double ITransform.FromSi(in Double value) => 1e1 * value;
    static String IRepresentable.Representation => "d";
}
[DebuggerDisplay(nameof(Centi))]
public readonly struct Centi : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e2;
    static Double ITransform.FromSi(in Double value) => 1e2 * value;
    static String IRepresentable.Representation => "c";
}
[DebuggerDisplay(nameof(Milli))]
public readonly struct Milli : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e3;
    static Double ITransform.FromSi(in Double value) => 1e3 * value;
    static String IRepresentable.Representation => "m";
}
[DebuggerDisplay(nameof(Micro))]
public readonly struct Micro : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e6;
    static Double ITransform.FromSi(in Double value) => 1e6 * value;
    static String IRepresentable.Representation => "μ";
}
[DebuggerDisplay(nameof(Nano))]
public readonly struct Nano : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e9;
    static Double ITransform.FromSi(in Double value) => 1e9 * value;
    static String IRepresentable.Representation => "n";
}
[DebuggerDisplay(nameof(Pico))]
public readonly struct Pico : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e12;
    static Double ITransform.FromSi(in Double value) => 1e12 * value;
    static String IRepresentable.Representation => "p";
}
[DebuggerDisplay(nameof(Femto))]
public readonly struct Femto : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e15;
    static Double ITransform.FromSi(in Double value) => 1e15 * value;
    static String IRepresentable.Representation => "f";
}
[DebuggerDisplay(nameof(Atto))]
public readonly struct Atto : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e18;
    static Double ITransform.FromSi(in Double value) => 1e18 * value;
    static String IRepresentable.Representation => "a";
}
[DebuggerDisplay(nameof(Zepto))]
public readonly struct Zepto : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e21;
    static Double ITransform.FromSi(in Double value) => 1e21 * value;
    static String IRepresentable.Representation => "z";
}
[DebuggerDisplay(nameof(Yocto))]
public readonly struct Yocto : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e24;
    static Double ITransform.FromSi(in Double value) => 1e24 * value;
    static String IRepresentable.Representation => "y";
}
[DebuggerDisplay(nameof(Ronto))]
public readonly struct Ronto : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e27;
    static Double ITransform.FromSi(in Double value) => 1e27 * value;
    static String IRepresentable.Representation => "r";
}
[DebuggerDisplay(nameof(Quecto))]
public readonly struct Quecto : ISiPrefix, IScaleDown
{
    static Double ITransform.ToSi(in Double value) => value / 1e30;
    static Double ITransform.FromSi(in Double value) => 1e30 * value;
    static String IRepresentable.Representation => "q";
}
