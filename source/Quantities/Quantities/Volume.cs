using System.Numerics;
using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities;

public readonly struct Volume : IQuantity<Volume>, IVolume
    , ICubic<Volume, IVolume, ILength>
    , IDivisionOperators<Volume, Area, Length>
    , IDivisionOperators<Volume, Length, Area>
{
    private readonly Quantity volume;
    internal Quantity Value => this.volume;
    Quantity IQuantity<Volume>.Value => this.volume;
    private Volume(in Quantity value) => this.volume = value;
    public Volume To<TLength>(in Cubic<TLength> other)
        where TLength : ILength, IUnit => new(other.Transform(in this.volume));
    public Volume To<TVolume>(in Scalar<TVolume> other)
        where TVolume : IVolume, IPowerOf<ILength>, IUnit => new(other.Transform(in this.volume, f => f.PowerOf<TVolume, ILength>()));
    public static Volume Of<TLength>(in Double value, in Cubic<TLength> measure)
        where TLength : ILength, IUnit => new(measure.Create(in value));
    public static Volume Of<TVolume>(in Double value, in Scalar<TVolume> measure)
        where TVolume : IVolume, IPowerOf<ILength>, IUnit => new(measure.Create(in value, f => f.PowerOf<TVolume, ILength>()));
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
    public static Boolean operator >(Volume left, Volume right) => left.volume > right.volume;
    public static Boolean operator >=(Volume left, Volume right) => left.volume >= right.volume;
    public static Boolean operator <(Volume left, Volume right) => left.volume < right.volume;
    public static Boolean operator <=(Volume left, Volume right) => left.volume <= right.volume;
    public static Volume operator +(Volume left, Volume right) => new(left.volume + right.volume);
    public static Volume operator -(Volume left, Volume right) => new(left.volume - right.volume);
    public static Volume operator *(Double scalar, Volume right) => new(scalar * right.volume);
    public static Volume operator *(Volume left, Double scalar) => new(scalar * left.volume);
    public static Volume operator /(Volume left, Double scalar) => new(left.volume / scalar);
    public static Double operator /(Volume left, Volume right) => left.volume.Ratio(in right.volume);

    public static Area operator /(Volume volume, Length length) => Area.From(in volume, in length);
    public static Length operator /(Volume volume, Area area) => Length.From(in volume, in area);
}
