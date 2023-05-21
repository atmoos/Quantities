using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;
using IDimension = Quantities.Dimensions.IDimension;

namespace Quantities.Factories;

public readonly struct Denominator<TFactory>
    where TFactory : ICreatable<TFactory>, IFactory
{
    private readonly ICreate creator;
    public TFactory Per => TFactory.Create(in this.creator);
    internal Denominator(in ICreate builder) => this.creator = builder;
}


public readonly struct Nominator<TQuantity, TCreate, TNominator, TFactory> : ICompoundFactory<Denominator<TFactory>, TNominator>
    where TNominator : IDimension, ILinear
    where TCreate : struct, ICreate<ICreate>
    where TFactory : ICreatable<TFactory>, IFactory
    where TQuantity : struct, IQuantity<TQuantity>, IDimension
{
    private readonly TCreate creator;
    internal Nominator(in TCreate creator) => this.creator = creator;
    public Denominator<TFactory> Imperial<TUnit>() where TUnit : IImperialUnit, TNominator
    {
        return new(this.creator.Create<Imperial<TUnit>>());
    }
    public Denominator<TFactory> Metric<TUnit>() where TUnit : IMetricUnit, TNominator
    {
        return new(this.creator.Create<Metric<TUnit>>());
    }
    public Denominator<TFactory> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TNominator
    {
        return new(this.creator.Create<Metric<TPrefix, TUnit>>());
    }
    public Denominator<TFactory> NonStandard<TUnit>() where TUnit : INoSystemUnit, TNominator
    {
        return new(this.creator.Create<NonStandard<TUnit>>());
    }
    public Denominator<TFactory> Si<TUnit>() where TUnit : ISiUnit, TNominator
    {
        return new(this.creator.Create<Si<TUnit>>());
    }
    public Denominator<TFactory> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TNominator
    {
        return new(this.creator.Create<Si<TPrefix, TUnit>>());
    }
}
