using Quantities.Creation;
using Quantities.Dimensions;
using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Units;

namespace Quantities;

public static class Systems
{
    public static Scalar<TDim> Si<TDim>()
        where TDim : ISiUnit, IDimension => new(Measure.Of<Si<TDim>>());
    public static Scalar<TDim> Si<TPrefix, TDim>()
        where TPrefix : IMetricPrefix
        where TDim : ISiUnit, IDimension => new(Measure.Of<Si<TPrefix, TDim>>());
    public static Scalar<TDim> Metric<TDim>()
        where TDim : IMetricUnit, IDimension => new(Measure.Of<Metric<TDim>>());
    public static Scalar<TDim> Metric<TPrefix, TDim>()
        where TPrefix : IMetricPrefix
        where TDim : IMetricUnit, IDimension => new(Measure.Of<Metric<TPrefix, TDim>>());
    public static Scalar<TDim> Imperial<TDim>()
        where TDim : IImperialUnit, IDimension => new(Measure.Of<Imperial<TDim>>());
    public static Scalar<TDim> NonStandard<TDim>()
        where TDim : INonStandardUnit, IDimension => new(Measure.Of<NonStandard<TDim>>());
    public static Square<TDim> Square<TDim>(in Scalar<TDim> scalar) => scalar.Square();
}
