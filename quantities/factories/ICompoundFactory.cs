using Quantities.Dimensions;

namespace Quantities.Factories;

public interface ICompoundFactory<out TQuantity, in TDimension>
    : ISiFactory<TQuantity, TDimension>, IMetricFactory<TQuantity, TDimension>, IImperialFactory<TQuantity, TDimension>, INonStandardFactory<TQuantity, TDimension>
    where TDimension : IDimension
{

}
