using System.Diagnostics;

namespace Quantities.Prefixes;

/*
kibi Ki 1024 2^10
mebi Mi 1048576 2^20
gibi Gi 1073741824 2^30
tebi Ti 1099511627776 2^40
pebi Pi 1125899906842624 2^50
exbi Ei 1152921504606846976 2^60
zebi Zi 1180591620717411303424 2^70
yobi Yi 1208925819614629174706176 2^80
*/

[DebuggerDisplay(nameof(Kibi))]
public readonly struct Kibi : IIecPrefix, IScaleUp
{
    internal const Double Factor = 1024;
    static Double ITransform.ToSi(in Double value) => Factor * value;
    static Double ITransform.FromSi(in Double value) => value / Factor;
    public static String Representation => "Ki";
}
[DebuggerDisplay(nameof(Mebi))]
public readonly struct Mebi : IIecPrefix, IScaleUp
{
    internal const Double Factor = 1048576;
    static Double ITransform.ToSi(in Double value) => Factor * value;
    static Double ITransform.FromSi(in Double value) => value / Factor;
    public static String Representation => "Mi";
}
[DebuggerDisplay(nameof(Gibi))]
public readonly struct Gibi : IIecPrefix, IScaleUp
{
    internal const Double Factor = 1073741824;
    static Double ITransform.ToSi(in Double value) => Factor * value;
    static Double ITransform.FromSi(in Double value) => value / Factor;
    public static String Representation => "Gi";
}
[DebuggerDisplay(nameof(Tebi))]
public readonly struct Tebi : IIecPrefix, IScaleUp
{
    internal const Double Factor = 1099511627776;
    static Double ITransform.ToSi(in Double value) => Factor * value;
    static Double ITransform.FromSi(in Double value) => value / Factor;
    public static String Representation => "Ti";
}
[DebuggerDisplay(nameof(Pebi))]
public readonly struct Pebi : IIecPrefix, IScaleUp
{
    internal const Double Factor = 1125899906842624;
    static Double ITransform.ToSi(in Double value) => Factor * value;
    static Double ITransform.FromSi(in Double value) => value / Factor;
    public static String Representation => "Pi";
}
[DebuggerDisplay(nameof(Exbi))]
public readonly struct Exbi : IIecPrefix, IScaleUp
{
    internal const Double Factor = 1152921504606846976;
    static Double ITransform.ToSi(in Double value) => Factor * value;
    static Double ITransform.FromSi(in Double value) => value / Factor;
    public static String Representation => "Ei";
}
[DebuggerDisplay(nameof(Zebi))]
public readonly struct Zebi : IIecPrefix, IScaleUp
{
    internal const Double Factor = Gibi.Factor * Tebi.Factor; // 1180591620717411303424
    static Double ITransform.ToSi(in Double value) => Factor * value;
    static Double ITransform.FromSi(in Double value) => value / Factor;
    public static String Representation => "Zi";
}
[DebuggerDisplay(nameof(Yobi))]
public readonly struct Yobi : IIecPrefix, IScaleUp
{
    internal const Double Factor = Tebi.Factor * Tebi.Factor; // 1208925819614629174706176
    static Double ITransform.ToSi(in Double value) => Factor * value;
    static Double ITransform.FromSi(in Double value) => value / Factor;
    public static String Representation => "Yi";
}