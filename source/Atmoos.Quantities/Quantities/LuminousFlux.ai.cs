using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public readonly struct LuminousFlux : IQuantity<LuminousFlux>, ILuminousFlux, IScalar<LuminousFlux, ILuminousFlux>
{
    private readonly Quantity luminousFlux;
    internal Quantity Value => this.luminousFlux;
    Quantity IQuantity<LuminousFlux>.Value => this.luminousFlux;

    private LuminousFlux(in Quantity value) => this.luminousFlux = value;

    public LuminousFlux To<TUnit>(in Scalar<TUnit> other)
        where TUnit : ILuminousFlux, IUnit => new(other.Transform(in this.luminousFlux));

    public static LuminousFlux Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : ILuminousFlux, IUnit => new(measure.Create(in value));

    static LuminousFlux IFactory<LuminousFlux>.Create(in Quantity value) => new(in value);

    public Boolean Equals(LuminousFlux other) => this.luminousFlux.Equals(other.luminousFlux);

    public override Boolean Equals(Object? obj) => obj is LuminousFlux luminousFlux && Equals(luminousFlux);

    public override Int32 GetHashCode() => this.luminousFlux.GetHashCode();

    public override String ToString() => this.luminousFlux.ToString();

    public String ToString(String? format, IFormatProvider? provider) => this.luminousFlux.ToString(format, provider);
}
