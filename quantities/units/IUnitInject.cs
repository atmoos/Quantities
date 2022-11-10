using Quantities.Dimensions;

namespace Quantities.Unit;

public interface IInject<TUnit, out T>
{
    T Inject<TInjectedUnit>(in Double value) where TInjectedUnit : TUnit, ITransform, IUnit;
}
public interface IUnitInject<TDimension>
    where TDimension : IDimension
{
    static abstract T Inject<T>(in IInject<TDimension, T> inject, in Double self);
}