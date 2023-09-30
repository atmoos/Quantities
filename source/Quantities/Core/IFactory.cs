namespace Quantities.Core;

public interface IFactory<out TResult>
{
    internal static abstract TResult Create(in Quantity value);
}
