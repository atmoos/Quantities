using System.Diagnostics;

namespace Atmoos.Quantities.Prefixes;

/* https://en.wikipedia.org/wiki/Metric_prefix
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
public readonly struct Quetta : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e30 * self;
    static String IRepresentable.Representation => "Q";
}
[DebuggerDisplay(nameof(Ronna))]
public readonly struct Ronna : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e27 * self;
    static String IRepresentable.Representation => "R";
}
[DebuggerDisplay(nameof(Yotta))]
public readonly struct Yotta : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e24 * self;
    static String IRepresentable.Representation => "Y";
}
[DebuggerDisplay(nameof(Zetta))]
public readonly struct Zetta : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e21 * self;
    static String IRepresentable.Representation => "Z";
}
[DebuggerDisplay(nameof(Exa))]
public readonly struct Exa : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e18 * self;
    static String IRepresentable.Representation => "E";
}
[DebuggerDisplay(nameof(Peta))]
public readonly struct Peta : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e15 * self;
    static String IRepresentable.Representation => "P";
}
[DebuggerDisplay(nameof(Tera))]
public readonly struct Tera : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e12 * self;
    static String IRepresentable.Representation => "T";
}
[DebuggerDisplay(nameof(Giga))]
public readonly struct Giga : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e9 * self;
    static String IRepresentable.Representation => "G";
}
[DebuggerDisplay(nameof(Mega))]
public readonly struct Mega : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e6 * self;
    static String IRepresentable.Representation => "M";
}
[DebuggerDisplay(nameof(Kilo))]
public readonly struct Kilo : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e3 * self;
    static String IRepresentable.Representation => "k";
}
[DebuggerDisplay(nameof(Hecto))]
public readonly struct Hecto : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e2 * self;
    static String IRepresentable.Representation => "h";
}
[DebuggerDisplay(nameof(Deca))]
public readonly struct Deca : IMetricPrefix, IScaleUp
{
    static Transformation ITransform.ToSi(Transformation self) => 1e1 * self;
    static String IRepresentable.Representation => "da";
}
[DebuggerDisplay(nameof(Deci))]
public readonly struct Deci : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e1;
    static String IRepresentable.Representation => "d";
}
[DebuggerDisplay(nameof(Centi))]
public readonly struct Centi : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e2;
    static String IRepresentable.Representation => "c";
}
[DebuggerDisplay(nameof(Milli))]
public readonly struct Milli : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e3;
    static String IRepresentable.Representation => "m";
}
[DebuggerDisplay(nameof(Micro))]
public readonly struct Micro : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e6;
    static String IRepresentable.Representation => "μ";
}
[DebuggerDisplay(nameof(Nano))]
public readonly struct Nano : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e9;
    static String IRepresentable.Representation => "n";
}
[DebuggerDisplay(nameof(Pico))]
public readonly struct Pico : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e12;
    static String IRepresentable.Representation => "p";
}
[DebuggerDisplay(nameof(Femto))]
public readonly struct Femto : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e15;
    static String IRepresentable.Representation => "f";
}
[DebuggerDisplay(nameof(Atto))]
public readonly struct Atto : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e18;
    static String IRepresentable.Representation => "a";
}
[DebuggerDisplay(nameof(Zepto))]
public readonly struct Zepto : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e21;
    static String IRepresentable.Representation => "z";
}
[DebuggerDisplay(nameof(Yocto))]
public readonly struct Yocto : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e24;
    static String IRepresentable.Representation => "y";
}
[DebuggerDisplay(nameof(Ronto))]
public readonly struct Ronto : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e27;
    static String IRepresentable.Representation => "r";
}
[DebuggerDisplay(nameof(Quecto))]
public readonly struct Quecto : IMetricPrefix, IScaleDown
{
    static Transformation ITransform.ToSi(Transformation self) => self / 1e30;
    static String IRepresentable.Representation => "q";
}
