using Quantities.Dimensions;

namespace Quantities.Measures;

internal interface ICreate<out TResult>
{
    TResult Create<TMeasure>(in Double value) where TMeasure : IMeasure;
}

public interface ICreate
{
    internal Quant Create<TMeasure>()
      where TMeasure : IMeasure, ILinear;
}

public interface IInjectCreate
{
    internal Quant Create<TMeasure, TAlias>()
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new();
}