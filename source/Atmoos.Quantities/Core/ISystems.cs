using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public interface ISystems<in TConstraint, out TResult>
{
    TResult Si<TUnit>()
        where TUnit : ISiUnit, TConstraint;
    TResult Metric<TUnit>()
        where TUnit : IMetricUnit, TConstraint;
    TResult Imperial<TUnit>()
        where TUnit : IImperialUnit, TConstraint;
    TResult NonStandard<TUnit>()
        where TUnit : INonStandardUnit, TConstraint;
}
