using Quantities.Dimensions;

namespace Quantities.Measures;

public interface ICreate
{
    internal Quant Create<TMeasure>()
      where TMeasure : IMeasure, ILinear;
}
internal interface ICreate<out TResult>
{
    internal TResult Create<TMeasure>()
      where TMeasure : IMeasure, ILinear;
}
public interface IAliasingCreate
{
    internal Quant Create<TMeasure, TAlias>()
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new();
}