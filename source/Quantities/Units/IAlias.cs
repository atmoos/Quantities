using Quantities.Dimensions;

namespace Quantities.Units;

public interface IAlias<out TDimension>
    where TDimension : IDimension
{
    static abstract T Inject<T>(ISystems<TDimension, T> basis);
}
