using Quantities.Units;

namespace Quantities;

// ToDo: Consider making this a purely static interface...
public interface ISystems<in TConstraint, out TResult>
{
    public TResult Si<TUnit>() where TUnit : ISiUnit, TConstraint;
    public TResult Metric<TUnit>() where TUnit : IMetricUnit, TConstraint;
    public TResult Imperial<TUnit>() where TUnit : IImperialUnit, TConstraint;
    public TResult NonStandard<TUnit>() where TUnit : INonStandardUnit, TConstraint;
}
