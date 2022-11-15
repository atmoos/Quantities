using Quantities.Dimensions;
using Quantities.Unit.Si;

namespace Quantities.Unit;

public interface IInject<TUnit, out T>
{
    T InjectSi<TInjectedUnit>(in Double value) where TInjectedUnit : TUnit, ISiBaseUnit;
    T InjectSiDerived<TInjectedUnit>(in Double value) where TInjectedUnit : TUnit, ISiDerivedUnit;
    T InjectSiAccepted<TInjectedUnit>(in Double value) where TInjectedUnit : TUnit, ISiAcceptedUnit;
    T InjectOther<TInjectedUnit>(in Double value) where TInjectedUnit : TUnit, ITransform, IUnit;
}
public interface IUnitInject<TDimension>
    where TDimension : IDimension
{
    static abstract T Inject<T>(in IInject<TDimension, T> inject, in Double self);
}