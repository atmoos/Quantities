using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;

namespace Quantities;

public readonly struct Volume : IQuantity<Volume>, IVolume
    , IFactory<ICubicFactory<Volume, IVolume, ILength>, Cube<To, Volume, IVolume, ILength>, Cube<Create, Volume, IVolume, ILength>>
    , IDivisionOperators<Volume, Area, Length>
    , IDivisionOperators<Volume, Length, Area>
{
    private readonly Quantity volume;
    internal Quantity Value => this.volume;
    Quantity IQuantity<Volume>.Value => this.volume;
    public Cube<To, Volume, IVolume, ILength> To => new(new To(in this.volume));
    private Volume(in Quantity value) => this.volume = value;
    public static Cube<Create, Volume, IVolume, ILength> Of(in Double value) => new(new Create(in value));
    static Volume IFactory<Volume>.Create(in Quantity value) => new(in value);
    internal static Volume Times(in Length length, in Area area) => new(length.Value * area.Value);
    internal static Volume Times(in Area area, in Length length) => new(area.Value * length.Value);
    public Boolean Equals(Volume other) => this.volume.Equals(other.volume);
    public override Boolean Equals(Object? obj) => obj is Volume Volume && Equals(Volume);
    public override Int32 GetHashCode() => this.volume.GetHashCode();
    public override String ToString() => this.volume.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.volume.ToString(format, provider);

    public static implicit operator Double(Volume Volume) => Volume.volume;
    public static Boolean operator ==(Volume left, Volume right) => left.Equals(right);
    public static Boolean operator !=(Volume left, Volume right) => !left.Equals(right);
    public static Volume operator +(Volume left, Volume right) => new(left.volume + right.volume);
    public static Volume operator -(Volume left, Volume right) => new(left.volume - right.volume);
    public static Volume operator *(Double scalar, Volume right) => new(scalar * right.volume);
    public static Volume operator *(Volume left, Double scalar) => new(scalar * left.volume);
    public static Volume operator /(Volume left, Double scalar) => new(left.volume / scalar);
    public static Double operator /(Volume left, Volume right) => left.volume.Divide(in right.volume);

    public static Area operator /(Volume volume, Length length) => Area.From(in volume, in length);
    public static Length operator /(Volume volume, Area area) => Length.From(in volume, area);
}
