using Quantities.Dimensions;

namespace Quantities.Factories;

public interface ICompoundFactory<out TResult, in TDimension>
    : ISiFactory<TResult, TDimension>, IMetricFactory<TResult, TDimension>, IImperialFactory<TResult, TDimension>, INonStandardFactory<TResult, TDimension>
    where TDimension : IDimension
{

}
