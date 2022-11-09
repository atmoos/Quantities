using System.Globalization;

namespace Quantities.Measures;

internal readonly struct Quant : IEquatable<Quant>, IFormattable
{
    private readonly Map map;
    public Double Value { get; }
    internal Quant(in Double value, in Map map)
    {
        this.map = map;
        this.Value = value;
    }

    public Double Project<TKernel>() where TKernel : IKernel
    {
        return this.map.Value<TKernel>(this.Value);
    }

    public Quant Times(in Quant other, in ICreate<Quant> creator)
    {
        Double product = this.Value * this.map.Project(other.map, other.Value);
        return this.map.Inject(new Creator<Quant>(in product, creator));
    }

    public Boolean Equals(Quant other)
    {
        const Double min = 1d - 2e-15;
        const Double max = 1d + 2e-15;
        Double quotient = this / other;
        return quotient is >= min and <= max;
    }

    public override Boolean Equals(Object? obj)
    {
        if (obj is Quant quant) {
            return Equals(quant);
        }
        return false;
    }

    public override Int32 GetHashCode() => this.Value.GetHashCode() ^ this.map.GetHashCode();
    public override String ToString() => ToString("g5", CultureInfo.CurrentCulture);
    public String ToString(String? format, IFormatProvider? provider) => this.map.Append(this.Value.ToString(format, provider));
    public static Boolean operator ==(Quant left, Quant right) => left.Equals(right);
    public static Boolean operator !=(Quant left, Quant right) => !left.Equals(right);
    public static Quant operator +(Quant left, Quant right)
    {
        var rightValue = left.map.Project(right.map, right.Value);
        return new(left.Value + rightValue, left.map);
    }
    public static Quant operator -(Quant left, Quant right)
    {
        var rightValue = left.map.Project(right.map, right.Value);
        return new(left.Value - rightValue, left.map);
    }
    public static Quant operator *(Double scalar, Quant right)
    {
        return new(scalar * right.Value, right.map);
    }
    public static Quant operator *(Quant left, Double scalar)
    {
        return new(scalar * left.Value, left.map);
    }
    public static Quant operator /(Quant left, Double scalar)
    {
        return new(left.Value / scalar, left.map);
    }
    public static Double operator /(Quant left, Quant right)
    {
        var rightValue = left.map.Project(right.map, right.Value);
        return left.Value / rightValue;
    }
}