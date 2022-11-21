using Quantities.Units;
using Quantities.Units.Si;

namespace Quantities.Measures;

internal interface IMeasure : ITransform, IRepresentable
{
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