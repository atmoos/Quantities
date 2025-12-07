using System.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Energy : IQuantity<Energy>, IEnergy
    , IProduct<Energy, IEnergy, IPower, ITime>
    , IDivisionOperators<Energy, Time, Power>
    , IDivisionOperators<Energy, Power, Time>
{
    private readonly Quantity energy;
    internal Quantity Value => this.energy;
    Quantity IQuantity<Energy>.Value => this.energy;
    private Energy(in Quantity value) => this.energy = value;
    public Energy To<TEnergy>(in Scalar<TEnergy> other)
        where TEnergy : IUnit, IEnergy => new(other.Transform(in this.energy));
    public Energy To<TPower, TTime>(in Product<TPower, TTime> other)
        where TPower : IUnit, IPower where TTime : IUnit, ITime => new(other.Transform(in this.energy));
    public static Energy Of<TEnergy>(in Double value, in Scalar<TEnergy> measure)
        where TEnergy : IUnit, IEnergy => new(measure.Create(in value));
    public static Energy Of<TPower, TTime>(in Double value, in Product<TPower, TTime> measure)
        where TPower : IUnit, IPower where TTime : IUnit, ITime => new(measure.Create(in value));
    static Energy IFactory<Energy>.Create(in Quantity value) => new(in value);
    internal static Energy From(in Power power, in Time time) => new(power.Value * time.Value);
    public Boolean Equals(Energy other) => this.energy.Equals(other.energy);
    public override Boolean Equals(Object? obj) => obj is Energy energy && Equals(energy);
    public override Int32 GetHashCode() => this.energy.GetHashCode();
    public override String ToString() => this.energy.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.energy.ToString(format, provider);

    public static Power operator /(Energy energy, Time time) => Power.From(in energy, in time);
    public static Time operator /(Energy energy, Power power) => Time.From(in energy, in power);
}
