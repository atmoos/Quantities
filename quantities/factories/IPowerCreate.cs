using Quantities.Dimensions;
using Quantities.Measures;

namespace Quantities.Factories;

public interface IPowerCreate<out TQuantity>
{
    internal TQuantity Create<TMeasure>()
      where TMeasure : IMeasure, ILinear;
    internal TQuantity Create<TMeasure, TAlias>()
      where TMeasure : IMeasure, ILinear where TAlias : IInjector, new();
}
