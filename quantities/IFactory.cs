using Quantities.Measures;

namespace Quantities;

public interface IFactory<out TResult>
{
    internal static abstract TResult Create(in Quant quant);
}
