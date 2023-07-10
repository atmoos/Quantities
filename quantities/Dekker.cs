using System.Diagnostics;
using static System.Math;

namespace Quantities;

// https://csclub.uwaterloo.ca/~pbarfuss/dekker1971.pdf
[DebuggerDisplay("{Value} | {Error}")]
public ref struct Dekker
{
    private const Double constant = 134217729d; // 2^(53 - 53 / 2) + 1;
    private Double value, error;
    internal readonly Double Value => this.value;
    internal readonly Double Error => this.error;
    internal Dekker(in Double value, in Double error = 0)
    {
        this.value = value;
        this.error = error;
    }

    public void Add(in Double other)
    {
        Double r = this.value + other;
        Double s = Abs(this.value) > Abs(other)
                    ? this.value - r + other + this.error
                    : other - r + this.value + this.error;
        this.value = r + s;
        this.error = r - this.value + s;
    }

    internal void Add(in Dekker other)
    {
        Double r = this.value + other.value;
        Double s = Abs(this.value) > Abs(other.value)
                    ? this.value - r + other.value + other.error + this.error
                    : other.value - r + this.value + this.error + other.error;
        this.value = r + s;
        this.error = r - this.value + s;
    }

    internal void Subtract(in Double other)
    {
        Double r = this.value - other;
        Double s = Abs(this.value) > Abs(other)
                    ? this.value - r - other + this.error
                    : -other - r + this.value + this.error;
        this.value = r + s;
        this.error = r - this.value + s;
    }

    internal void Subtract(in Dekker other)
    {
        Double r = this.value - other.value;
        Double s = Abs(this.value) > Abs(other.value)
                    ? this.value - r - other.value - other.error + this.error
                    : -other.value - r + this.value + this.error - other.error;
        this.value = r + s;
        this.error = r - this.value + s;
    }
    public void Multiply(in Double other)
    {
        var (c, cc) = ExactMultiply(in this.value, in other);
        cc += this.error * other;
        this.value = c + cc;
        this.error = c - this.value + cc;
    }
    internal void Multiply(in Dekker other)
    {
        var (c, cc) = ExactMultiply(in this.value, in other.value);
        cc += this.value * other.error + this.error * other.value;
        this.value = c + cc;
        this.error = c - this.value + cc;
    }
    public void Divide(in Double other)
    {
        Double c = this.value / other;
        var (u, uu) = ExactMultiply(in c, in other);
        Double cc = (this.value - u - uu + this.error) / other;
        this.value = c + cc;
        this.error = c - this.value + cc;
    }
    internal void Divide(in Dekker other)
    {
        Double c = this.value / other.value;
        var (u, uu) = ExactMultiply(in c, in other.value);
        Double cc = (this.value - u - uu + this.error - c * other.error) / other.value;
        this.value = c + cc;
        this.error = c - this.value + cc;
    }

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
