using Quantities.Measures;

namespace Quantities.Quantities.Builders;

public interface IBuilder<out TQuantity>
    where TQuantity : struct, IQuantity<TQuantity>, Dimensions.IDimension
{
    internal Quant By<TMeasure>() where TMeasure : IMeasure;
}