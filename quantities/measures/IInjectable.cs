using Quantities.Dimensions;

namespace Quantities.Measures;

internal interface ICreate<out T>
{
    T Create<TMeasure>(in Double value) where TMeasure : IMeasure;
}

internal interface ICreateLinear<out T>
{
    T Create<TMeasure>(in Double value) where TMeasure : IMeasure, ILinear;
}

internal interface IInjectLinear
{
    static abstract T Inject<T>(in ICreateLinear<T> create, in Double value);
}