using System.Numerics;
using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Energy : IQuantity<Energy>, IEnergy
    , IScalar<Energy, IEnergy>
    , IDivisionOperators<Energy, Time, Power>
    , IDivisionOperators<Energy, Power, Time>
{
    private readonly Quantity energy;
    internal Quantity Value => this.energy;
    Quantity IQuantity<Energy>.Value => this.energy;
    public Energy To<TEnergy>(in Scalar<TEnergy> energy)
        where TEnergy : IUnit, IEnergy => new(energy.Transform(in this.energy));
    public Energy To<TPower, TTime>(in Product<TPower, TTime> energy)
        where TPower : IUnit, IPower where TTime : IUnit, ITime => new(energy.Transform(in this.energy));
    private Energy(in Quantity value) => this.energy = value;
    public static Energy Of<TEnergy>(in Double value, in Scalar<TEnergy> energy)
        where TEnergy : IUnit, IEnergy => new(energy.Create(in value));
    public static Energy Of<TPower, TTime>(in Double value, in Product<TPower, TTime> energy)
        where TPower : IUnit, IPower where TTime : IUnit, ITime => new(energy.Create(in value));
    static Energy IFactory<Energy>.Create(in Quantity value) => new(in value);
    internal static Energy From(in Power power, in Time time) => new(power.Value * time.Value);
    public Boolean Equals(Energy other) => this.energy.Equals(other.energy);
    public override Boolean Equals(Object? obj) => obj is Energy energy && Equals(energy);
    public override Int32 GetHashCode() => this.energy.GetHashCode();
    public override String ToString() => this.energy.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.energy.ToString(format, provider);

    public static implicit operator Double(Energy energy) => energy.energy;
    public static Boolean operator ==(Energy left, Energy right) => left.Equals(right);
    public static Boolean operator !=(Energy left, Energy right) => !left.Equals(right);
    public static Energy operator +(Energy left, Energy right) => new(left.energy + right.energy);
    public static Energy operator -(Energy left, Energy right) => new(left.energy - right.energy);
    public static Energy operator *(Double scalar, Energy right) => new(scalar * right.energy);
    public static Energy operator *(Energy left, Double scalar) => new(scalar * left.energy);
    public static Energy operator /(Energy left, Double scalar) => new(left.energy / scalar);
    public static Double operator /(Energy left, Energy right) => left.energy.Divide(in right.energy);

    public static Power operator /(Energy energy, Time time) => Power.From(in energy, in time);
    public static Time operator /(Energy energy, Power power) => Time.From(in energy, in power);
}
