using Quantities.Measures;

namespace Quantities;

public interface IQuantFactory<out TSelf>
{
    internal static abstract TSelf Create(in Quant quant);
}
