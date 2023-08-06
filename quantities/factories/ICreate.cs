using Quantities.Measures;

namespace Quantities.Factories;

public interface ICreate
{
    internal Quant Create<TMeasure>()
      where TMeasure : IMeasure;
    internal Quant Create<TMeasure, TAlias>()
      where TMeasure : IMeasure where TAlias : IInjector, new();
}
