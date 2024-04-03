using Quantities.Dimensions;

namespace Quantities.Units;

public interface ISystemInject<out TDimension>
    where TDimension : IDimension
{
    static abstract T Inject<T>(ISystems<TDimension, T> basis);
}

public interface IPowerOf<out TDimension> : ISystemInject<TDimension>
    where TDimension : IDimension
{ /* marker */ }

public interface IInvertible<out TDimension> : ISystemInject<TDimension>
    where TDimension : IDimension
{ /* marker */ }
