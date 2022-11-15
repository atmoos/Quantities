using System.Globalization;

namespace Quantities.Measures;

internal readonly struct Quant : IEquatable<Quant>, IFormattable
{
    private readonly Map map;
    private readonly Double value;
    public Double Value => this.value;
    internal Quant(in Double value, in Map map)
    {
        this.map = map;
        this.value = value;
    }
    public Double Project<TMeasure>() where TMeasure : IMeasure => this.map.MapTo<TMeasure>(in this.value);
    public Double Project(in Quant other) => ReferenceEquals(this.map, other.map)
        ? other.value : this.map.Project(other.map, in other.value);
    public Quant Transform(in ICreate<Quant> transformation) => this.map.Inject(in transformation, in this.value);
    public Quant PseudoMultiply(in Quant right)
    {
        var projected = Project(in right);
        return new(this.value * projected, this.map);
    }
    public Quant PseudoDivide(in Quant denominator)
    {
        var projected = Project(in denominator);
        return new(this.value / projected, this.map);
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

    public override Int32 GetHashCode() => this.value.GetHashCode() ^ this.map.GetHashCode();
    public override String ToString() => ToString("g5", CultureInfo.CurrentCulture);
    public String ToString(String? format, IFormatProvider? provider) => $"{this.value.ToString(format, provider)} {this.map.Representation}";
    public static Boolean operator ==(Quant left, Quant right) => left.Equals(right);
    public static Boolean operator !=(Quant left, Quant right) => !left.Equals(right);
    public static Quant operator +(Quant left, Quant right)
    {
        var rightValue = left.Project(in right);
        return new(left.value + rightValue, left.map);
    }
    public static Quant operator -(Quant left, Quant right)
    {
        var rightValue = left.Project(in right);
        return new(left.value - rightValue, left.map);
    }
    public static Quant operator *(Double scalar, Quant right)
    {
        return new(scalar * right.value, right.map);
    }
    public static Quant operator *(Quant left, Double scalar)
    {
        return new(scalar * left.value, left.map);
    }
    public static Quant operator /(Quant left, Double scalar)
    {
        return new(left.value / scalar, left.map);
    }
    public static Double operator /(Quant left, Quant right)
    {
        var rightValue = left.Project(in right);
        return left.Value / rightValue;
    }
}
