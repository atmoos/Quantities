using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;

namespace Quantities.Quantities;

public readonly struct Energy : IQuantity<Energy>, IEnergy
    , IFactory<IProductFactory<IEnergy, IPower, ITime>, Product<To, Energy, IEnergy, IPower, ITime>, Product<Create, Energy, IEnergy, IPower, ITime>>
    , IDivisionOperators<Energy, Time, Power>
    , IDivisionOperators<Energy, Power, Time>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<Energy>.Value => this.quant;
    public Product<To, Energy, IEnergy, IPower, ITime> To => new(new To(in this.quant));
    private Energy(in Quant quant) => this.quant = quant;
    public static Product<Create, Energy, IEnergy, IPower, ITime> Of(in Double value) => new(new Create(in value));
    static Energy IFactory<Energy>.Create(in Quant quant) => new(in quant);
    internal static Energy From(in Power power, in Time time) => new(power.Quant.Multiply(Metric.TriadicScaling, time.Quant));
    public Boolean Equals(Energy other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Energy energy && Equals(energy);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Energy left, Energy right) => left.Equals(right);
    public static Boolean operator !=(Energy left, Energy right) => !left.Equals(right);
    public static implicit operator Double(Energy energy) => energy.quant.Value;
    public static Energy operator +(Energy left, Energy right) => new(left.quant + right.quant);
    public static Energy operator -(Energy left, Energy right) => new(left.quant - right.quant);
    public static Energy operator *(Double scalar, Energy right) => new(scalar * right.quant);
    public static Energy operator *(Energy left, Double scalar) => new(scalar * left.quant);
    public static Energy operator /(Energy left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Energy left, Energy right) => left.quant / right.quant;
    public static Power operator /(Energy left, Time right) => Power.From(in left, in right);
    public static Time operator /(Energy left, Power right) => Time.From(in left, in right);
}
