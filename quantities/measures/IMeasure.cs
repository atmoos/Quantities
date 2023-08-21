using Quantities.Prefixes;
using Quantities.Units.Imperial;
using Quantities.Units.NonStandard;
using Quantities.Units.Si;

namespace Quantities.Measures;

internal interface IMeasure : ITransform, IRepresentable, ISerialize
{
    static abstract IOperations Operations { get; }
    static abstract Boolean Is<TMeasure>() where TMeasure : IMeasure;
    static abstract Boolean HasSame<TDimension>() where TDimension : Dimensions.IDimension;
    static abstract T Normalize<T>(IPrefixScale scaling, IInject<T> inject, in Double value);
    static abstract (Double, T) Lower<T>(IInject<T> inject, in Double value);
}

internal interface ISiMeasure<in TUnit> : IMeasure
    where TUnit : ISiUnit
{
    /* marker interface*/
}
/// <summary>
/// SI accepts certain units for use in the SI system. However they are not regarded as actual SI units. 
/// </summary>
/// <remarks>
/// An example of this is the litre which is defined in terms of the metre as: 1000 ℓ in 1 m³
/// </remarks>
internal interface IMetricMeasure<in TUnit> : IMeasure
    where TUnit : IMetricUnit
{
    /* marker interface*/
}
internal interface IImperialMeasure<in TUnit> : IMeasure
    where TUnit : IImperialUnit, ITransform, IRepresentable
{
    /* marker interface*/
}
internal interface INonStandardMeasure<in TUnit> : IMeasure
    where TUnit : INoSystemUnit, ITransform, IRepresentable
{
    /* marker interface*/
}