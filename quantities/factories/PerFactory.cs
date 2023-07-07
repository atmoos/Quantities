using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;
using IDimension = Quantities.Dimensions.IDimension;

namespace Quantities.Factories;

public interface IPerFactory<TNominator, TDenominator> : IFactory
    where TNominator : IDimension, ILinear
    where TDenominator : IDimension, ILinear
{

}

// ToDo: Make this a ref struct...
public readonly struct Denominator<TCreate, TResult, TDenominator>
    where TResult : IFactory<TResult>
    where TCreate : struct, ICreate
    where TDenominator : IDimension, ILinear
{
    public CompoundFactory<TCreate, TResult, TDenominator> Per { get; }
    internal Denominator(in TCreate creator, IInject<TCreate> inject) => this.Per = new(in creator, inject);
}

public readonly struct PerFactory<TCreate, TResult, TNominator, TDenominator> : ICompoundFactory<Denominator<TCreate, TResult, TDenominator>, TNominator>, IPerFactory<TNominator, TDenominator>
    where TResult : IFactory<TResult>
    where TCreate : struct, ICreate
    where TNominator : IDimension, ILinear
    where TDenominator : IDimension, ILinear
{
    private readonly TCreate creator;
    internal PerFactory(in TCreate creator) => this.creator = creator;
    public Denominator<TCreate, TResult, TDenominator> Imperial<TUnit>() where TUnit : IImperialUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Imperial<TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> Metric<TUnit>() where TUnit : IMetricUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Metric<TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Metric<TPrefix, TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> NonStandard<TUnit>() where TUnit : INoSystemUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, NonStandard<TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> Si<TUnit>() where TUnit : ISiUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Si<TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TNominator => new(in this.creator, AllocationFree<QuotientInjector<TCreate, Si<TPrefix, TUnit>>>.Item);
}