using Quantities.Measures;

namespace Quantities.Quantities.Builders;

public interface IBuilder<out TQuantity>
    where TQuantity : Dimensions.IDimension, IEquatable<TQuantity>, IFormattable
{
    internal Quant By<TMeasure>() where TMeasure : IMeasure;
    internal Quant By<TMeasure, TInject>()
    where TMeasure : IMeasure
    where TInject : IInjector, new();
}