using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct ElectricCurrent : IQuantity<ElectricCurrent>, IElectricCurrent
    , IFactory<ISiFactory<ElectricCurrent, IElectricCurrent>, SiOnly<To, ElectricCurrent, IElectricCurrent>, SiOnly<Create, ElectricCurrent, IElectricCurrent>>
    , IMultiplyOperators<ElectricCurrent, ElectricPotential, Power>
    , IMultiplyOperators<ElectricCurrent, ElectricalResistance, ElectricPotential>
{
    private static readonly IRoot root = new SiRoot<Ampere>();
    private readonly Quantity current;
    internal Quantity Value => this.current;
    Quantity IQuantity<ElectricCurrent>.Value => this.current;
    public SiOnly<To, ElectricCurrent, IElectricCurrent> To => new(new To(in this.current));
    private ElectricCurrent(in Quantity value) => this.current = value;
    public static SiOnly<Create, ElectricCurrent, IElectricCurrent> Of(in Double value) => new(new Create(in value));
    static ElectricCurrent IFactory<ElectricCurrent>.Create(in Quantity value) => new(in value);
    internal static ElectricCurrent From(in ElectricPotential potential, in ElectricalResistance resistance)
    {
        return new(MetricPrefix.ScaleThree(potential.Value.SiDivide(resistance.Value), root));
    }
    internal static ElectricCurrent From(in Power power, in ElectricPotential potential)
    {
        return new(MetricPrefix.ScaleThree(power.Value.SiDivide(potential.Value), root));
    }

    public Boolean Equals(ElectricCurrent other) => this.current.Equals(other.current);
    public String ToString(String? format, IFormatProvider? provider) => this.current.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricCurrent current && Equals(current);
    public override Int32 GetHashCode() => this.current.GetHashCode();
    public override String ToString() => this.current.ToString();

    public static Boolean operator ==(ElectricCurrent left, ElectricCurrent right) => left.Equals(right);
    public static Boolean operator !=(ElectricCurrent left, ElectricCurrent right) => !left.Equals(right);
    public static implicit operator Double(ElectricCurrent current) => current.current;
    public static ElectricCurrent operator +(ElectricCurrent left, ElectricCurrent right) => new(left.current + right.current);
    public static ElectricCurrent operator -(ElectricCurrent left, ElectricCurrent right) => new(left.current - right.current);
    public static ElectricCurrent operator *(Double scalar, ElectricCurrent right) => new(scalar * right.current);
    public static ElectricCurrent operator *(ElectricCurrent left, Double scalar) => new(scalar * left.current);
    public static ElectricCurrent operator /(ElectricCurrent left, Double scalar) => new(left.current / scalar);
    public static Double operator /(ElectricCurrent left, ElectricCurrent right) => left.current / right.current;

    // Ohm's Law
    public static ElectricPotential operator *(ElectricCurrent current, ElectricalResistance resistance) => ElectricPotential.From(in current, in resistance);
    public static Power operator *(ElectricCurrent left, ElectricPotential right) => Power.From(in right, in left);
}
