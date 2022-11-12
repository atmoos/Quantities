using System.Globalization;
using Quantities.Measures.Transformations;

namespace Quantities.Measures;

internal readonly struct Quant : IEquatable<Quant>, IFormattable
{
    private static readonly ICreate<Quant> lower = new LinearMap<Quant>(new LowerToLinear());
    private static readonly ICreate<Quant> square = new LinearMap<Quant>(new RaiseTo<Square>());
    private readonly Map map;
    private readonly Double value;
    public Double Value => this.value;
    internal Quant(in Double value, in Map map)
    {
        this.map = map;
        this.value = value;
    }

    public Double Project<TMeasure>() where TMeasure : IMeasure
    {
        return this.map.MapTo<TMeasure>(in this.value);
    }

    public Quant PseudoDivision(in Quant denominator)
    {
        var lowered = this.map.Inject(new Creator<Quant>(in this.value, in lower));
        return new Quant(lowered / denominator, in lowered.map);
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
        var rightValue = left.map.Project(right.map, in right.value);
        return new(left.value + rightValue, left.map);
    }
    public static Quant operator -(Quant left, Quant right)
    {
        var rightValue = left.map.Project(right.map, in right.value);
        return new(left.value - rightValue, left.map);
    }
    public static Quant operator *(in Quant left, in Quant right)
    {
        var product = left.value * left.map.Project(right.map, in right.value);
        return left.map.Inject(new Creator<Quant>(in product, in square));
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
        var rightValue = left.map.Project(right.map, in right.value);
        return left.Value / rightValue;
    }
}