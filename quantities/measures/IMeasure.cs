using Quantities.Unit;
using Quantities.Unit.Si;

namespace Quantities.Measures;

internal interface IMeasure : ITransform, IRepresentable
{
    static abstract T Inject<T>(in ICreate<T> create, in Double value);
}
internal interface ISiMeasure<in TUnit> : IMeasure
    where TUnit : ISiUnit
{
    /* marker interface*/
}
internal interface ISiAccepted<in TUnit> : IMeasure
    where TUnit : ISiAcceptedUnit
{
    /* marker interface*/
}
internal interface IOtherMeasure<in TUnit> : IMeasure
    where TUnit : IUnit, ITransform, IRepresentable
{
    /* marker interface*/
}