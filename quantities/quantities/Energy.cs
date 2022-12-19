using System.Numerics;
using Quantities.Dimensions;
using Quantities.Factories;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Quantities;

public readonly struct Energy : IQuantity<Energy>, IEnergy<Mass, Length, Time>
    , IFactory<ICompoundFactory<Energy, IEnergy<Mass, Length, Time>>, Energy.Factory<LinearTo>, Energy.Factory<LinearCreate>>
    , IDivisionOperators<Energy, Time, Power>
    , IDivisionOperators<Energy, Power, Time>
{
    private readonly Quant quant;
    internal Quant Quant => this.quant;
    public Factory<LinearTo> To => new(new LinearTo(in this.quant));
    private Energy(in Quant quant) => this.quant = quant;
    public static Factory<LinearCreate> Of(in Double value) => new(new LinearCreate(in value));
    internal static Energy From(in Power power, in Time time) => new(power.Quant.Multiply(time.Quant));
    void IQuantity<Energy>.Serialize(IWriter writer) => this.quant.Write(writer);

    public Boolean Equals(Energy other) => this.quant.Equals(other.quant);
    public override Boolean Equals(Object? obj) => obj is Energy energy && Equals(energy);
    public override Int32 GetHashCode() => this.quant.GetHashCode();
    public override String ToString() => this.quant.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.quant.ToString(format, provider);

    public static Boolean operator ==(Energy left, Energy right) => left.Equals(right);
    public static Boolean operator !=(Energy left, Energy right) => !left.Equals(right);
    public static implicit operator Double(Energy energy) => energy.quant.Value;
    public static Energy operator +(Energy left, Energy right) => new(left.quant + right.quant);
    public static Energy operator -(Energy left, Energy right) => new(left.quant - right.quant);
    public static Energy operator *(Double scalar, Energy right) => new(scalar * right.quant);
    public static Energy operator *(Energy left, Double scalar) => new(scalar * left.quant);
    public static Energy operator /(Energy left, Double scalar) => new(left.quant / scalar);
    public static Double operator /(Energy left, Energy right) => left.quant / right.quant;
    public static Power operator /(Energy left, Time right) => Power.From(in left, in right);
    public static Time operator /(Energy left, Power right) => Time.From(in left, in right);

    public readonly struct Factory<TCreate> : ICompoundFactory<Energy, IEnergy>
        where TCreate : ICreate
    {
        private readonly TCreate creator;
        internal Factory(TCreate creator) => this.creator = creator;
        public Energy Imperial<TUnit>() where TUnit : IImperialUnit, IEnergy => new(this.creator.Create<Imperial<TUnit>>());
        public Energy Metric<TUnit>() where TUnit : IMetricUnit, IEnergy => new(this.creator.Create<Metric<TUnit>>());
        public Energy Metric<TPrefix, TUnit>()
            where TPrefix : IMetricPrefix
            where TUnit : IMetricUnit, IEnergy => new(this.creator.Create<Metric<TPrefix, TUnit>>());
        public Energy Metric<TPrefix, TPowerUnit, TTimeUnit>()
            where TPrefix : IMetricPrefix
            where TPowerUnit : ISiUnit, IPower
            where TTimeUnit : IMetricUnit, ITime => new(this.creator.Create<Product<Si<TPrefix, TPowerUnit>, Metric<TTimeUnit>>>());
        public Energy NonStandard<TUnit>() where TUnit : INoSystemUnit, IEnergy => new(this.creator.Create<NonStandard<TUnit>>());
        public Energy Si<TUnit>() where TUnit : ISiUnit, IEnergy => new(this.creator.Create<Si<TUnit>>());
        public Energy Si<TPrefix, TUnit>()
            where TPrefix : IMetricPrefix
            where TUnit : ISiUnit, IEnergy => new(this.creator.Create<Si<TPrefix, TUnit>>());
        public Energy Si<TPrefix, TPowerUnit, TTimeUnit>()
            where TPrefix : IMetricPrefix
            where TPowerUnit : ISiUnit, IPower
            where TTimeUnit : ISiUnit, ITime => new(this.creator.Create<Product<Si<TPrefix, TPowerUnit>, Si<TTimeUnit>>>());
        public Energy Si<TPowerPrefix, TPowerUnit, TTimePrefix, TTimeUnit>()
            where TPowerPrefix : IPrefix
            where TPowerUnit : ISiUnit, IPower
            where TTimePrefix : IPrefix
            where TTimeUnit : ISiUnit, ITime => new(this.creator.Create<Product<Si<TPowerPrefix, TPowerUnit>, Si<TTimePrefix, TTimeUnit>>>());
    }
}