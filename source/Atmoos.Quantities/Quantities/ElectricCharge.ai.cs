using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct ElectricCharge : IQuantity<ElectricCharge>, IElectricCharge, IProduct<ElectricCharge, IElectricCharge, IElectricCurrent, ITime>
{
    private readonly Quantity electricCharge;
    internal Quantity Value => this.electricCharge;
    Quantity IQuantity<ElectricCharge>.Value => this.electricCharge;

    private ElectricCharge(in Quantity value) => this.electricCharge = value;

    public ElectricCharge To<TElectricCharge>(in Scalar<TElectricCharge> other)
        where TElectricCharge : IElectricCharge, IUnit => new(other.Transform(in this.electricCharge));

    public ElectricCharge To<TElectricCurrent, TTime>(in Product<TElectricCurrent, TTime> other)
        where TElectricCurrent : IElectricCurrent, IUnit
        where TTime : ITime, IUnit => new(other.Transform(in this.electricCharge));

    public static ElectricCharge Of<TElectricCharge>(in Double value, in Scalar<TElectricCharge> measure)
        where TElectricCharge : IElectricCharge, IUnit => new(measure.Create(in value));

    public static ElectricCharge Of<TElectricCurrent, TTime>(in Double value, in Product<TElectricCurrent, TTime> measure)
        where TElectricCurrent : IElectricCurrent, IUnit
        where TTime : ITime, IUnit => new(measure.Create(in value));

    static ElectricCharge IFactory<ElectricCharge>.Create(in Quantity value) => new(in value);

    public Boolean Equals(ElectricCharge other) => this.electricCharge.Equals(other.electricCharge);

    public override Boolean Equals(Object? obj) => obj is ElectricCharge electricCharge && Equals(electricCharge);

    public override Int32 GetHashCode() => this.electricCharge.GetHashCode();

    public override String ToString() => this.electricCharge.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.electricCharge.ToString(format, provider);
}
