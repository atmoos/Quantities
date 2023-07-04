namespace Quantities.Measures;

public interface ICreate
{
    internal Quant Create<TMeasure>()
      where TMeasure : IMeasure;
    internal Quant Create<TMeasure, TAlias>()
      where TMeasure : IMeasure where TAlias : IInjector, new();
}