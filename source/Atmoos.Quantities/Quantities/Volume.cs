using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Volume : IQuantity<Volume>, IVolume
    , IPowerOf<Volume, IVolume, ILength, Three>
{
    private readonly Quantity volume;
    internal Quantity Value => this.volume;
    Quantity IQuantity<Volume>.Value => this.volume;
    private Volume(in Quantity value) => this.volume = value;
    public Volume To<TLength>(in Power<TLength, Three> other)
        where TLength : ILength, IUnit => new(other.Transform(in this.volume));
    public Volume To<TVolume>(in Scalar<TVolume> other)
        where TVolume : IVolume, IPowerOf<ILength>, IUnit => new(other.Transform(in this.volume, static f => ref f.AliasOf<TVolume, ILength>()));
    public static Volume Of<TLength>(in Double value, in Power<TLength, Three> measure)
        where TLength : ILength, IUnit => new(measure.Create(in value));
    public static Volume Of<TVolume>(in Double value, in Scalar<TVolume> measure)
        where TVolume : IVolume, IPowerOf<ILength>, IUnit => new(measure.Create(in value, static f => ref f.AliasOf<TVolume, ILength>()));
    static Volume IFactory<Volume>.Create(in Quantity value) => new(in value);
    internal static Volume Times(in Length length, in Area area) => new(length.Value * area.Value);
    internal static Volume Times(in Area area, in Length length) => new(area.Value * length.Value);
    public Boolean Equals(Volume other) => this.volume.Equals(other.volume);
    public override Boolean Equals(Object? obj) => obj is Volume Volume && Equals(Volume);
    public override Int32 GetHashCode() => this.volume.GetHashCode();
    public override String ToString() => this.volume.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.volume.ToString(format, provider);
}
