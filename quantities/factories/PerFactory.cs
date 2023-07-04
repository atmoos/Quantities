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
    private readonly TCreate creator;
    private readonly INewInject inject;
    public CompoundFactory<TCreate, TResult, TDenominator> Per => new(in this.creator, this.inject);
    internal Denominator(in TCreate creator, INewInject inject)
    {
        this.creator = creator;
        this.inject = inject;
    }
}

public readonly struct PerFactory<TCreate, TResult, TNominator, TDenominator> : ICompoundFactory<Denominator<TCreate, TResult, TDenominator>, TNominator>, IPerFactory<TNominator, TDenominator>
    where TResult : IFactory<TResult>
    where TCreate : struct, ICreate
    where TNominator : IDimension, ILinear
    where TDenominator : IDimension, ILinear
{
    private readonly TCreate creator;
    internal PerFactory(in TCreate creator) => this.creator = creator;
    public Denominator<TCreate, TResult, TDenominator> Imperial<TUnit>() where TUnit : IImperialUnit, TNominator => new(in this.creator, ZeroAllocation<PerInjector<Imperial<TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> Metric<TUnit>() where TUnit : IMetricUnit, TNominator => new(in this.creator, ZeroAllocation<PerInjector<Metric<TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> Metric<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : IMetricUnit, TNominator => new(in this.creator, ZeroAllocation<PerInjector<Metric<TPrefix, TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> NonStandard<TUnit>() where TUnit : INoSystemUnit, TNominator => new(in this.creator, ZeroAllocation<PerInjector<NonStandard<TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> Si<TUnit>() where TUnit : ISiUnit, TNominator => new(in this.creator, ZeroAllocation<PerInjector<Si<TUnit>>>.Item);
    public Denominator<TCreate, TResult, TDenominator> Si<TPrefix, TUnit>()
        where TPrefix : IMetricPrefix
        where TUnit : ISiUnit, TNominator => new(in this.creator, ZeroAllocation<PerInjector<Si<TPrefix, TUnit>>>.Item);
}

internal sealed class PerInjector<TNominator> : INewInject
    where TNominator : IMeasure
{
    public Quant Inject<TCreate, TMeasure>(in TCreate create)
        where TCreate : struct, ICreate
        where TMeasure : IMeasure => create.Create<Quotient<TNominator, TMeasure>>();

    public Quant Inject<TCreate, TMeasure, TAlias>(in TCreate create)
        where TCreate : struct, ICreate
        where TMeasure : IMeasure
        where TAlias : IInjector, new() => throw new ShouldBeUnusedException(this);
}