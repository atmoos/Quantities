using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public readonly struct VolumetricFlowRate : IQuantity<VolumetricFlowRate>, IVolumetricFlowRate, IQuotient<VolumetricFlowRate, IVolumetricFlowRate, IVolume, ITime>
{
    private readonly Quantity volumetricFlowRate;
    internal Quantity Value => this.volumetricFlowRate;
    Quantity IQuantity<VolumetricFlowRate>.Value => this.volumetricFlowRate;

    private VolumetricFlowRate(in Quantity value) => this.volumetricFlowRate = value;

    public VolumetricFlowRate To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IVolumetricFlowRate, IUnit => new(other.Transform(in this.volumetricFlowRate));

    public VolumetricFlowRate To<TNominator, TDenominator>(in Quotient<TNominator, TDenominator> other)
        where TNominator : IVolume, IUnit
        where TDenominator : ITime, IUnit => new(other.Transform(in this.volumetricFlowRate));

    public static VolumetricFlowRate Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IVolumetricFlowRate, IUnit => new(measure.Create(in value));
    public static VolumetricFlowRate Of<TNominator, TDenominator>(in Double value, in Quotient<TNominator, TDenominator> measure)
        where TNominator : IVolume, IUnit
        where TDenominator : ITime, IUnit => new(measure.Create(in value));

    static VolumetricFlowRate IFactory<VolumetricFlowRate>.Create(in Quantity value) => new(in value);

    public Boolean Equals(VolumetricFlowRate other) => this.volumetricFlowRate.Equals(other.volumetricFlowRate);

    public override Boolean Equals(Object? obj) => obj is VolumetricFlowRate volumetricFlowRate && Equals(volumetricFlowRate);

    public override Int32 GetHashCode() => this.volumetricFlowRate.GetHashCode();

    public override String ToString() => this.volumetricFlowRate.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.volumetricFlowRate.ToString(format, provider);
}
