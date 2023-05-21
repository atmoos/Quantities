using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Quantities.Builders;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;
using IDimension = Quantities.Dimensions.IDimension;

namespace Quantities.Factories;

public interface IBuilderCreate<out TSelf, TQuantity>
    where TSelf : IBuilderCreate<TSelf, TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, IDimension
{
    internal static abstract TSelf Create(in IBuilder<TQuantity> builder);
}

public readonly struct Denominator<TQuantity, TFactory>
    where TFactory : IBuilderCreate<TFactory, TQuantity>, IFactory
    where TQuantity : struct, IQuantity<TQuantity>, IDimension
{
    private readonly IBuilder<TQuantity> builder;
    public TFactory Per => TFactory.Create(in this.builder);
    internal Denominator(in IBuilder<TQuantity> builder) => this.builder = builder;
}


public readonly struct Factory<TQuantity, TDenominator> : ICompoundFactory<TQuantity, TDenominator>, IBuilderCreate<Factory<TQuantity, TDenominator>, TQuantity>
    where TDenominator : IDimension, ILinear
    where TQuantity : struct, IQuantity<TQuantity>, IDimension, IFactory<TQuantity>
{
    private readonly IBuilder<TQuantity> builder;
    private Factory(in IBuilder<TQuantity> builder) => this.builder = builder;
    static Factory<TQuantity, TDenominator> IBuilderCreate<Factory<TQuantity, TDenominator>, TQuantity>.Create(in IBuilder<TQuantity> builder) => new(in builder);

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

public readonly struct Nominator<TQuantity, TCreate, TNominator, TFactory> : ICompoundFactory<Denominator<TQuantity, TFactory>, TNominator>
    where TNominator : IDimension, ILinear
    where TCreate : struct, ICreate<IBuilder<TQuantity>>
    where TFactory : IBuilderCreate<TFactory, TQuantity>, IFactory
    where TQuantity : struct, IQuantity<TQuantity>, IDimension
{
    private readonly TCreate creator;
    internal Nominator(in TCreate creator) => this.creator = creator;
    public Denominator<TQuantity, TFactory> Imperial<TUnit>() where TUnit : IImperialUnit, TNominator
    {
        return new(this.creator.Create<Imperial<TUnit>>());
    }
    public Denominator<TQuantity, TFactory> Metric<TUnit>() where TUnit : IMetricUnit, TNominator
    {
        return new(this.creator.Create<Metric<TUnit>>());
    }
    public Denominator<TQuantity, TFactory> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TNominator
    {
        return new(this.creator.Create<Metric<TPrefix, TUnit>>());
    }
    public Denominator<TQuantity, TFactory> NonStandard<TUnit>() where TUnit : INoSystemUnit, TNominator
    {
        return new(this.creator.Create<NonStandard<TUnit>>());
    }
    public Denominator<TQuantity, TFactory> Si<TUnit>() where TUnit : ISiUnit, TNominator
    {
        return new(this.creator.Create<Si<TUnit>>());
    }
    public Denominator<TQuantity, TFactory> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TNominator
    {
        return new(this.creator.Create<Si<TPrefix, TUnit>>());
    }
}
