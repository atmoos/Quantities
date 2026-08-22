using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct LuminousIntensity : IQuantity<LuminousIntensity>, ILuminousIntensity, IScalar<LuminousIntensity, ILuminousIntensity>
{
    private readonly Quantity luminousIntensity;
    internal Quantity Value => this.luminousIntensity;
    Quantity IQuantity<LuminousIntensity>.Value => this.luminousIntensity;

    private LuminousIntensity(in Quantity value) => this.luminousIntensity = value;

    public LuminousIntensity To<TUnit>(in Scalar<TUnit> other)
        where TUnit : ILuminousIntensity, IUnit => new(other.Transform(in this.luminousIntensity));

    public static LuminousIntensity Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : ILuminousIntensity, IUnit => new(measure.Create(in value));

    static LuminousIntensity IFactory<LuminousIntensity>.Create(in Quantity value) => new(in value);

    public Boolean Equals(LuminousIntensity other) => this.luminousIntensity.Equals(other.luminousIntensity);

    public override Boolean Equals(Object? obj) => obj is LuminousIntensity luminousIntensity && Equals(luminousIntensity);

    public override Int32 GetHashCode() => this.luminousIntensity.GetHashCode();

    public override String ToString() => this.luminousIntensity.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.luminousIntensity.ToString(format, provider);
}
