using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Quantities.Numerics;

// https://csclub.uwaterloo.ca/~pbarfuss/dekker1971.pdf
[DebuggerDisplay("{Value} | {Correction}")]
internal struct Dekker : IUnaryNegationOperators<Dekker, Dekker>
{
    private const Double constant = 134217729d; // 2^(53 - 53 / 2) + 1;
    private Double value, correction;
    internal readonly Double Value => this.value;
    internal readonly Double Correction => this.correction;
    internal Dekker(in Double value, in Double correction = 0)
    {
        this.value = value;
        this.correction = correction;
    }

    public void Add(in Double other)
    {
        Double r = this.value + other;
        Double s = Abs(this.value) > Abs(other)
                    ? this.value - r + other + this.correction
                    : other - r + this.value + this.correction;
        (this.value, this.correction) = Set(in r, in s);
    }

    internal void Add(in Dekker other)
    {
        Double r = this.value + other.value;
        Double s = Abs(this.value) > Abs(other.value)
                    ? this.value - r + other.value + other.correction + this.correction
                    : other.value - r + this.value + this.correction + other.correction;
        (this.value, this.correction) = Set(in r, in s);
    }

    internal void Subtract(in Double other)
    {
        Double r = this.value - other;
        Double s = Abs(this.value) > Abs(other)
                    ? this.value - r - other + this.correction
                    : -other - r + this.value + this.correction;
        (this.value, this.correction) = Set(in r, in s);
    }

    internal void Subtract(in Dekker other)
    {
        Double r = this.value - other.value;
        Double s = Abs(this.value) > Abs(other.value)
                    ? this.value - r - other.value - other.correction + this.correction
                    : -other.value - r + this.value + this.correction - other.correction;
        (this.value, this.correction) = Set(in r, in s);
    }

    public void Multiply(in Double other)
    {
        var (c, cc) = ExactMultiply(in this.value, in other);
        cc += this.correction * other;
        (this.value, this.correction) = Set(in c, in cc);
    }

    internal void Multiply(in Dekker other)
    {
        var (c, cc) = ExactMultiply(in this.value, in other.value);
        cc += this.value * other.correction + this.correction * other.value;
        (this.value, this.correction) = Set(in c, in cc);
    }

    public void Divide(in Double other)
    {
        Double c = this.value / other;
        var (u, uu) = ExactMultiply(in c, in other);
        Double cc = (this.value - u - uu + this.correction) / other;
        (this.value, this.correction) = Set(in c, in cc);
    }

    internal void Divide(in Dekker other)
    {
        Double c = this.value / other.value;
        var (u, uu) = ExactMultiply(in c, in other.value);
        Double cc = (this.value - u - uu + this.correction - c * other.correction) / other.value;
        (this.value, this.correction) = Set(in c, in cc);
    }

    public static Dekker operator -(Dekker value) => new(-value.value, -value.correction);

    [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.AggressiveInlining)]
    private static (Double value, Double correction) Set(in Double value, in Double correction)
    {
        var newValue = value + correction;
        return (newValue, value - newValue + correction);
    }

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
