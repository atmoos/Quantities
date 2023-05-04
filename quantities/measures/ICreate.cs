using Quantities.Dimensions;

namespace Quantities.Measures;

internal interface ICreate<out TResult>
{
    TResult Create<TMeasure>(in Double value) where TMeasure : IMeasure;
}

public interface ILinearCreate<out TResult>
{
    internal TResult Create<TMeasure>()
      where TMeasure : IMeasure, ILinear;
}

public interface ILinearInjectCreate<out TResult>
{
    internal TResult Create<TMeasure, TAlias>()
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new();
}