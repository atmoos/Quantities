using Quantities.Measures;
using Quantities.Prefixes;
using Quantities.Unit;
using Quantities.Unit.Si;

namespace Quantities.Quantities.Builders;

public interface IBuilder<out TQuantity>
    where TQuantity : Dimensions.IDimension, IEquatable<TQuantity>, IFormattable
{
    internal Quant By<TUnit>()
        where TUnit : IUnit, ITransform;
    internal Quant By<TPrefix, TUnit>()
        where TPrefix : IPrefix
        where TUnit : ISiUnit;
}