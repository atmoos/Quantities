using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public readonly struct MagneticFlux : IQuantity<MagneticFlux>, IMagneticFlux, IProduct<MagneticFlux, IMagneticFlux, IElectricPotential, ITime>
{
    private readonly Quantity magneticFlux;
    internal Quantity Value => this.magneticFlux;
    Quantity IQuantity<MagneticFlux>.Value => this.magneticFlux;

    private MagneticFlux(in Quantity value) => this.magneticFlux = value;

    public MagneticFlux To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IMagneticFlux, IUnit => new(other.Transform(in this.magneticFlux));

    public MagneticFlux To<TElectricPotential, TTime>(in Product<TElectricPotential, TTime> other)
        where TElectricPotential : IElectricPotential, IUnit
        where TTime : ITime, IUnit => new(other.Transform(in this.magneticFlux));

    public static MagneticFlux Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IMagneticFlux, IUnit => new(measure.Create(in value));

    public static MagneticFlux Of<TElectricPotential, TTime>(in Double value, in Product<TElectricPotential, TTime> measure)
        where TElectricPotential : IElectricPotential, IUnit
        where TTime : ITime, IUnit => new(measure.Create(in value));

    static MagneticFlux IFactory<MagneticFlux>.Create(in Quantity value) => new(in value);

    public Boolean Equals(MagneticFlux other) => this.magneticFlux.Equals(other.magneticFlux);

    public override Boolean Equals(Object? obj) => obj is MagneticFlux magneticFlux && Equals(magneticFlux);

    public override Int32 GetHashCode() => this.magneticFlux.GetHashCode();

    public override String ToString() => this.magneticFlux.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.magneticFlux.ToString(format, provider);
}
