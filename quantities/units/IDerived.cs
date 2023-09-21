using Quantities.Dimensions;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Units;

public interface IDerived<TDimension>
    where TDimension : IDimension
{
    static abstract Transformation Derived(in From<TDimension> from);
}

public readonly ref struct From<TDimension>
    where TDimension : IDimension
{
    private readonly Transformation transformation;
    internal From(Transformation transformation) => this.transformation = transformation;
    public Transformation Imperial<TUnit>()
        where TUnit : IImperialUnit, TDimension => TUnit.ToSi(this.transformation);

    public Transformation Metric<TUnit>()
        where TUnit : IMetricUnit<TUnit, TDimension>, TDimension => TUnit.ToSi(this.transformation);

    public Transformation NonStandard<TUnit>()
        where TUnit : INoSystemUnit, TDimension => TUnit.ToSi(this.transformation);

    public Transformation Si<TUnit>()
        where TUnit : ISiUnit, TDimension => this.transformation;
}
