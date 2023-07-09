using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Measures.Transformations;
using Quantities.Prefixes;
using Quantities.Quantities.Roots;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Length : IQuantity<Length>, ILength
    , IFactory<Length>
    , IFactory<ICompoundFactory<Length, ILength>, Linear<To, Length, ILength>, Linear<Create, Length, ILength>>
    , IMultiplyOperators<Length, Length, Area>
    , IMultiplyOperators<Length, Area, Volume>
    , IDivisionOperators<Length, Time, Velocity>
{
    private static readonly IRoot root = new SiRoot<Metre>();
    private static readonly Measures.IInject<Quant> linear = new ToLinear();
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    Quant IQuantity<Length>.Value => this.quant;
    public Linear<To, Length, ILength> To => new(new To(in this.quant));
    private Length(in Quant quant) => this.quant = quant;
    public static Linear<Create, Length, ILength> Of(in Double value) => new(new Create(in value));
    static Length IFactory<Length>.Create(in Quant quant) => new(in quant);
    internal static Length From(in Area area, in Length length)
    {
        var pseudoArea = area.Quant.Transform(in linear);
        return new(pseudoArea.PseudoDivide(length.Quant));
    }
    internal static Length From(in Velocity velocity, in Time time)
    {
        // ToDo: Recover length units form velocity
        return new(MetricPrefix.Scale(velocity.Quant.SiMultiply(time.Quant), root));
    }
    internal static Length From(in Volume volume, in Area area)
    {
        var pseudoArea = area.Quant.Transform(in linear);
        var pseudoVolume = volume.Quant.Transform(in linear);
        return new(pseudoVolume.PseudoDivide(in pseudoArea));
    }

    public Boolean Equals(Length other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Length length && Equals(length);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Length left, Length right) => left.Equals(right);
    public static Boolean operator !=(Length left, Length right) => !left.Equals(right);
    public static implicit operator Double(Length length) => length.quant.Value;
    public static Length operator +(Length left, Length right) => new(left.quant + right.quant);
    public static Length operator -(Length left, Length right) => new(left.quant - right.quant);
    public static Area operator *(Length left, Length right) => Area.From(in left, in right);
    public static Volume operator *(Length length, Area area) => Volume.Times(length, area);
    public static Length operator *(Double scalar, Length right) => new(scalar * right.quant);
    public static Length operator *(Length left, Double scalar) => new(scalar * left.quant);
    public static Length operator /(Length left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Length left, Length right) => left.quant / right.quant;
    public static Velocity operator /(Length left, Time right) => Velocity.From(in left, in right);
}