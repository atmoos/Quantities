using Quantities.Measures;

namespace Quantities;

public interface IQuantityFactory<out TSelf, in TDimension>
    where TDimension : Dimensions.IDimension
    where TSelf : struct, IQuantity<TSelf>, TDimension
{
    internal Quant Quant { get; }
    internal static abstract TSelf Create(in Quant quant);
}
