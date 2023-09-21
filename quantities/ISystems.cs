using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities;

public interface ISystems<in TConstraint, out TResult>
{
    public TResult Si<TInjectedUnit>() where TInjectedUnit : ISiUnit, TConstraint;
    public TResult Metric<TInjectedUnit>() where TInjectedUnit : IMetricUnit, TConstraint;
    public TResult Imperial<TInjectedUnit>() where TInjectedUnit : IImperialUnit, TConstraint;
    public TResult NonStandard<TInjectedUnit>() where TInjectedUnit : INoSystemUnit, TConstraint;
}
