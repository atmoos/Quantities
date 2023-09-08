using Quantities.Measures;

namespace Quantities.Factories;

public interface ICreate
{
    internal Quantity Create<TMeasure>()
      where TMeasure : IMeasure;
    internal Quantity Create<TMeasure, TAlias>()
      where TMeasure : IMeasure where TAlias : IInjector, new();
}
