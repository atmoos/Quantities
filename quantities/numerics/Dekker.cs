using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Quantities.Numerics;

// https://csclub.uwaterloo.ca/~pbarfuss/dekker1971.pdf
[DebuggerDisplay("{Value} | {Correction}")]
internal readonly struct Dekker
    : IAdditionOperators<Dekker, Dekker, Dekker>
    , IAdditionOperators<Dekker, Double, Dekker>
    , ISubtractionOperators<Dekker, Dekker, Dekker>
    , ISubtractionOperators<Dekker, Double, Dekker>
    , IMultiplyOperators<Dekker, Dekker, Dekker>
    , IMultiplyOperators<Dekker, Double, Dekker>
    , IDivisionOperators<Dekker, Dekker, Dekker>
    , IDivisionOperators<Dekker, Double, Dekker>
    , IUnaryNegationOperators<Dekker, Dekker>
    , ICastOperators<Dekker>
{
    private const Double constant = 134217729d; // 2^(53 - 53 / 2) + 1;
    private readonly Double value, correction;
    internal Dekker(in Double value)
    {
        this.value = value;
        this.correction = 0d;
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    private Dekker(in Double result, in Double compensator)
    {
        var newValue = this.value = result + compensator;
        this.correction = result - newValue + compensator;
    }

    public static Dekker operator -(Dekker value) => new(-value.value, -value.correction);

    public static Dekker operator +(Dekker left, Dekker right)
    {
        Double r = left.value + right.value;
        Double s = Abs(left.value) > Abs(right.value)
                    ? left.value - r + right.value + right.correction + left.correction
                    : right.value - r + left.value + left.correction + right.correction;
        return new(in r, in s);
    }

    public static Dekker operator +(Dekker left, Double right)
    {
        Double r = left.value + right;
        Double s = Abs(left.value) > Abs(right)
                    ? left.value - r + right + left.correction
                    : right - r + left.value + left.correction;
        return new(in r, in s);
    }

    public static Dekker operator -(Dekker left, Dekker right)
    {
        Double r = left.value - right.value;
        Double s = Abs(left.value) > Abs(right.value)
                    ? left.value - r - right.value - right.correction + left.correction
                    : -right.value - r + left.value + left.correction - right.correction;
        return new(in r, in s);
    }

    public static Dekker operator -(Dekker left, Double right)
    {
        Double r = left.value - right;
        Double s = Abs(left.value) > Abs(right)
                    ? left.value - r - right + left.correction
                    : -right - r + left.value + left.correction;
        return new(in r, in s);
    }

    public static Dekker operator *(Dekker left, Dekker right)
    {
        var (c, cc) = ExactMultiply(in left.value, in right.value);
        cc += left.value * right.correction + left.correction * right.value;
        return new(in c, in cc);
    }

    public static Dekker operator *(Dekker left, Double right)
    {
        var (c, cc) = ExactMultiply(in left.value, in right);
        cc += left.correction * right;
        return new(in c, in cc);
    }

    public static Dekker operator *(Double left, Dekker right)
    {
        var (c, cc) = ExactMultiply(in left, in right.value);
        cc += left * right.correction;
        return new(in c, in cc);
    }

    public static Dekker operator /(Dekker left, Dekker right)
    {
        Double c = left.value / right.value;
        var (u, uu) = ExactMultiply(in c, in right.value);
        Double cc = (left.value - u - uu + left.correction - c * right.correction) / right.value;
        return new(in c, in cc);
    }

    public static Dekker operator /(Dekker left, Double right)
    {
        Double c = left.value / right;
        var (u, uu) = ExactMultiply(in c, in right);
        Double cc = (left.value - u - uu + left.correction) / right;
        return new(in c, in cc);
    }

    public static implicit operator Double(Dekker self) => self.value;

    [MethodImpl(MethodImplOptions.NoOptimization)]
    private static (Double value, Double error) ExactMultiply(in Double x, in Double y)
    {
        Double p = x * constant;
        Double q = y * constant;
        Double hx = x - p + p;
        Double tx = x - hx;
        Double hy = y - q + q;
        Double ty = y - hy;

        p = hx * hy;
        q = hx * ty + tx * hy;
        Double z = p + q;
        return (z, p - z + q + tx * ty);
    }
}
