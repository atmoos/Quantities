using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si.Derived;

namespace Quantities.Quantities;

public readonly struct Power : IQuantity<Power>, IPower
    , IFactory<IDefaultFactory<Power, IPower>, Linear<To, Power, IPower>, Linear<Create, Power, IPower>>
    , IMultiplyOperators<Power, Time, Energy>
    , IDivisionOperators<Power, ElectricCurrent, ElectricPotential>
    , IDivisionOperators<Power, ElectricPotential, ElectricCurrent>
    , IDivisionOperators<Power, Force, Velocity>
    , IDivisionOperators<Power, Velocity, Force>
{
    private static readonly IRoot root = new SiRoot<Watt>();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<Power>.Value => this.quant;
    public Linear<To, Power, IPower> To => new(new To(in this.quant));
    private Power(in Quant quant) => this.quant = quant;
    public static Linear<Create, Power, IPower> Of(in Double value) => new(new Create(in value));
    static Power IFactory<Power>.Create(in Quant quant) => new(in quant);
    internal static Power From(in ElectricPotential potential, in ElectricCurrent current)
    {
        return new(potential.Quant.SiMultiply(current.Quant));
    }
    internal static Power From(in Force force, in Velocity velocity)
    {
        return new(force.Quant.SiMultiply(velocity.Quant));
    }
    internal static Power From(in Energy energy, in Time time)
    {
        return new(MetricPrefix.ScaleThree(energy.Quant.SiDivide(time.Quant), root));
    }

    public Boolean Equals(Power other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Power power && Equals(power);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Power left, Power right) => left.Equals(right);
    public static Boolean operator !=(Power left, Power right) => !left.Equals(right);
    public static implicit operator Double(Power power) => power.quant.Value;
    public static Power operator +(Power left, Power right) => new(left.quant + right.quant);
    public static Power operator -(Power left, Power right) => new(left.quant - right.quant);
    public static Power operator *(Double scalar, Power right) => new(scalar * right.quant);
    public static Power operator *(Power left, Double scalar) => new(scalar * left.quant);
    public static Power operator /(Power left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Power left, Power right) => left.quant / right.quant;

    public static ElectricPotential operator /(Power power, ElectricCurrent current) => ElectricPotential.From(in power, in current);
    public static ElectricCurrent operator /(Power power, ElectricPotential potential) => ElectricCurrent.From(in power, in potential);
    public static Velocity operator /(Power power, Force force) => Velocity.From(in power, in force);
    public static Force operator /(Power power, Velocity velocity) => Force.From(in power, in velocity);
    public static Energy operator *(Power left, Time right) => Energy.From(in left, in right);
}
