using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;
using IDimension = Quantities.Dimensions.IDimension;

namespace Quantities.Factories;

public interface IQuotientFactory<TDimension, TNominator, TDenominator> : IFactory
    where TDimension : IQuotient<TNominator, TDenominator>, IDimension
    where TNominator : IDimension, ILinear
    where TDenominator : IDimension, ILinear
{
}

public readonly struct Denominator<TCreate, TQuantity, TDenominatorDimension>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>
    where TDenominatorDimension : IDimension, ILinear
{
    public Compound<TCreate, TQuantity, TDenominatorDimension> Per { get; }
    internal Denominator(in TCreate creator, IInject<TCreate> inject) => this.Per = new(in creator, inject);
}

public readonly struct Quotient<TCreate, TQuantity, TDimension, TNominator, TDenominator> : ICompoundFactory<Denominator<TCreate, TQuantity, TDenominator>, TNominator>, IQuotientFactory<TDimension, TNominator, TDenominator>
    where TCreate : struct, ICreate
    where TQuantity : IFactory<TQuantity>, TDimension
    where TDimension : IQuotient<TNominator, TDenominator>, IDimension
    where TNominator : IDimension, ILinear
    where TDenominator : IDimension, ILinear
{
    private readonly TCreate creator;
    public Linear<TCreate, TQuantity, TDimension> Linear => new(in this.creator);
    internal Quotient(in TCreate creator) => this.creator = creator;
    public Denominator<TCreate, TQuantity, TDenominator> Imperial<TUnit>() where TUnit : IImperialUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Imperial<TUnit>>>.Item);
    public Denominator<TCreate, TQuantity, TDenominator> Metric<TUnit>() where TUnit : IMetricUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Metric<TUnit>>>.Item);
    public Denominator<TCreate, TQuantity, TDenominator> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Metric<TPrefix, TUnit>>>.Item);
    public Denominator<TCreate, TQuantity, TDenominator> NonStandard<TUnit>() where TUnit : INoSystemUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, NonStandard<TUnit>>>.Item);
    public Denominator<TCreate, TQuantity, TDenominator> Si<TUnit>() where TUnit : ISiUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Si<TUnit>>>.Item);
    public Denominator<TCreate, TQuantity, TDenominator> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Si<TPrefix, TUnit>>>.Item);
}