using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;

namespace Quantities.Quantities;

public readonly struct Energy : IQuantity<Energy>, IEnergy
    , IFactory<IProductFactory<IEnergy, IPower, ITime>, Product<To, Energy, IEnergy, IPower, ITime>, Product<Create, Energy, IEnergy, IPower, ITime>>
    , IDivisionOperators<Energy, Time, Power>
    , IDivisionOperators<Energy, Power, Time>
{
    private readonly Quantity energy;
    internal Quantity Value => this.energy;
    Quantity IQuantity<Energy>.Value => this.energy;
    public Product<To, Energy, IEnergy, IPower, ITime> To => new(new To(in this.energy));
    private Energy(in Quantity value) => this.energy = value;
    public static Product<Create, Energy, IEnergy, IPower, ITime> Of(in Double value) => new(new Create(in value));
    static Energy IFactory<Energy>.Create(in Quantity value) => new(in value);
    internal static Energy From(in Power power, in Time time) => new(power.Value.Multiply(time.Value));

    public Boolean Equals(Energy other) => this.energy.Equals(other.energy);
    public override Boolean Equals(Object? obj) => obj is Energy energy && Equals(energy);
    public override Int32 GetHashCode() => this.energy.GetHashCode();
    public override String ToString() => this.energy.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.energy.ToString(format, provider);

    public static Boolean operator ==(Energy left, Energy right) => left.Equals(right);
    public static Boolean operator !=(Energy left, Energy right) => !left.Equals(right);
    public static implicit operator Double(Energy energy) => energy.energy;
    public static Energy operator +(Energy left, Energy right) => new(left.energy + right.energy);
    public static Energy operator -(Energy left, Energy right) => new(left.energy - right.energy);
    public static Energy operator *(Double scalar, Energy right) => new(scalar * right.energy);
    public static Energy operator *(Energy left, Double scalar) => new(scalar * left.energy);
    public static Energy operator /(Energy left, Double scalar) => new(left.energy / scalar);
    public static Double operator /(Energy left, Energy right) => left.energy / right.energy;
    public static Power operator /(Energy left, Time right) => Power.From(in left, in right);
    public static Time operator /(Energy left, Power right) => Time.From(in left, in right);
}
