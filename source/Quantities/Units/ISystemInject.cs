using Quantities.Dimensions;

namespace Quantities.Units;

public interface ISystemInject<out TDimension>
    where TDimension : IDimension
{
    static abstract T Inject<T>(ISystems<TDimension, T> basis);
}

// ToDo: Consider renaming this "PowerOf", or similar. Alias is a rather silly name...
public interface IAlias<out TDimension> : ISystemInject<TDimension>
    where TDimension : IDimension
{ /* marker */ }

public interface IInvertible<out TDimension> : ISystemInject<TDimension>
    where TDimension : IDimension
{ /* marker */ }
