using Quantities.Dimensions;

namespace Quantities.Units;

public interface IAlias { /* marker */}

public interface IAlias<TDimension> : IAlias
    where TDimension : IDimension
{
    static abstract T Inject<T>(ISystems<TDimension, T> basis);
}
