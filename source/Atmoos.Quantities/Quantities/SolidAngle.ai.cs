using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct SolidAngle : IQuantity<SolidAngle>, ISolidAngle, IScalar<SolidAngle, ISolidAngle>
{
    private readonly Quantity solidAngle;
    internal Quantity Value => this.solidAngle;
    Quantity IQuantity<SolidAngle>.Value => this.solidAngle;

    private SolidAngle(in Quantity value) => this.solidAngle = value;

    public SolidAngle To<TUnit>(in Scalar<TUnit> other)
        where TUnit : ISolidAngle, IUnit => new(other.Transform(in this.solidAngle));

    public static SolidAngle Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : ISolidAngle, IUnit => new(measure.Create(in value));

    static SolidAngle IFactory<SolidAngle>.Create(in Quantity value) => new(in value);

    public Boolean Equals(SolidAngle other) => this.solidAngle.Equals(other.solidAngle);

    public override Boolean Equals(Object? obj) => obj is SolidAngle solidAngle && Equals(solidAngle);

    public override Int32 GetHashCode() => this.solidAngle.GetHashCode();

    public override String ToString() => this.solidAngle.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.solidAngle.ToString(format, provider);
}
