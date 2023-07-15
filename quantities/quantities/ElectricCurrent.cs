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
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<ElectricCurrent>.Value => this.quant;
    public SiOnly<To, ElectricCurrent, IElectricCurrent> To => new(new To(in this.quant));
    private ElectricCurrent(in Quant quant) => this.quant = quant;
    public static SiOnly<Create, ElectricCurrent, IElectricCurrent> Of(in Double value) => new(new Create(in value));
    static ElectricCurrent IFactory<ElectricCurrent>.Create(in Quant quant) => new(in quant);
    internal static ElectricCurrent From(in ElectricPotential potential, in ElectricalResistance resistance)
    {
        return new(MetricPrefix.ScaleThree(potential.Quant.SiDivide(resistance.Quant), root));
    }
    internal static ElectricCurrent From(in Power power, in ElectricPotential potential)
    {
        return new(MetricPrefix.ScaleThree(power.Quant.SiDivide(potential.Quant), root));
    }

    public Boolean Equals(ElectricCurrent other) => this.quant.Equals(other.quant);
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);
    public override Boolean Equals(Object? obj) => obj is ElectricCurrent current && Equals(current);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();

    public static Boolean operator ==(ElectricCurrent left, ElectricCurrent right) => left.Equals(right);
    public static Boolean operator !=(ElectricCurrent left, ElectricCurrent right) => !left.Equals(right);
    public static implicit operator Double(ElectricCurrent current) => current.quant.Value;
    public static ElectricCurrent operator +(ElectricCurrent left, ElectricCurrent right) => new(left.quant + right.quant);
    public static ElectricCurrent operator -(ElectricCurrent left, ElectricCurrent right) => new(left.quant - right.quant);
    public static ElectricCurrent operator *(Double scalar, ElectricCurrent right) => new(scalar * right.quant);
    public static ElectricCurrent operator *(ElectricCurrent left, Double scalar) => new(scalar * left.quant);
    public static ElectricCurrent operator /(ElectricCurrent left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(ElectricCurrent left, ElectricCurrent right) => left.quant / right.quant;

    // Ohm's Law
    public static ElectricPotential operator *(ElectricCurrent current, ElectricalResistance resistance) => ElectricPotential.From(in current, in resistance);
    public static Power operator *(ElectricCurrent left, ElectricPotential right) => Power.From(in right, in left);
}
