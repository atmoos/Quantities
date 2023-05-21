using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Builders;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;
using IDimension = Quantities.Dimensions.IDimension;

namespace Quantities.Factories;

public readonly struct Denominator<TQuantity, TDimension, TDenominator>
    where TDimension : IDimension
    where TDenominator : IDimension, ILinear
    where TQuantity : struct, IQuantity<TQuantity>, TDimension, IFactory<TQuantity>
{
    private readonly IBuilder<TQuantity> builder;
    public Factory Per => new(in this.builder);
    internal Denominator(in IBuilder<TQuantity> builder) => this.builder = builder;

    public readonly struct Factory : ICompoundFactory<TQuantity, TDenominator>
    {
        private readonly IBuilder<TQuantity> builder;
        internal Factory(in IBuilder<TQuantity> builder) => this.builder = builder;
        public TQuantity Imperial<TUnit>() where TUnit : IImperialUnit, TDenominator
        {
            return TQuantity.Create(this.builder.By<Imperial<TUnit>>());
        }
        public TQuantity Metric<TUnit>() where TUnit : IMetricUnit, TDenominator
        {
            return TQuantity.Create(this.builder.By<Metric<TUnit>>());
        }
        public TQuantity Metric<TPrefix, TUnit>()
            where TPrefix : IMetricPrefix
            where TUnit : IMetricUnit, TDenominator
        {
            return TQuantity.Create(this.builder.By<Metric<TPrefix, TUnit>>());
        }
        public TQuantity NonStandard<TUnit>() where TUnit : INoSystemUnit, TDenominator
        {
            return TQuantity.Create(this.builder.By<NonStandard<TUnit>>());
        }
        public TQuantity Si<TUnit>() where TUnit : ISiUnit, TDenominator
        {
            return TQuantity.Create(this.builder.By<Si<TUnit>>());
        }
        public TQuantity Si<TPrefix, TUnit>()
            where TPrefix : IMetricPrefix
            where TUnit : ISiUnit, TDenominator
        {
            return TQuantity.Create(this.builder.By<Si<TPrefix, TUnit>>());
        }
    }
}

public readonly struct Nominator<TQuantity, TDim, TCreate, TNominator, TDenominator> : ICompoundFactory<Denominator<TQuantity, TDim, TDenominator>, TNominator>
    where TNominator : IDimension, ILinear
    where TDenominator : IDimension, ILinear
    where TDim : IPer<TNominator, TDenominator>
    where TCreate : struct, ICreate<IBuilder<TQuantity>>
    where TQuantity : struct, IQuantity<TQuantity>, TDim, IFactory<TQuantity>
{
    private readonly TCreate creator;
    internal Nominator(in TCreate creator) => this.creator = creator;
    public Denominator<TQuantity, TDim, TDenominator> Imperial<TUnit>() where TUnit : IImperialUnit, TNominator
    {
        return new(this.creator.Create<Imperial<TUnit>>());
    }
    public Denominator<TQuantity, TDim, TDenominator> Metric<TUnit>() where TUnit : IMetricUnit, TNominator
    {
        return new(this.creator.Create<Metric<TUnit>>());
    }
    public Denominator<TQuantity, TDim, TDenominator> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TNominator
    {
        return new(this.creator.Create<Metric<TPrefix, TUnit>>());
    }
    public Denominator<TQuantity, TDim, TDenominator> NonStandard<TUnit>() where TUnit : INoSystemUnit, TNominator
    {
        return new(this.creator.Create<NonStandard<TUnit>>());
    }
    public Denominator<TQuantity, TDim, TDenominator> Si<TUnit>() where TUnit : ISiUnit, TNominator
    {
        return new(this.creator.Create<Si<TUnit>>());
    }
    public Denominator<TQuantity, TDim, TDenominator> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TNominator
    {
        return new(this.creator.Create<Si<TPrefix, TUnit>>());
    }
}
