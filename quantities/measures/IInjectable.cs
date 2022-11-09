using Quantities.Dimensions;
using Quantities.Measures.Imperial;
using Quantities.Measures.Si;

namespace Quantities.Measures;

internal interface ICreate<out T>
{
    T CreateSi<TSi>(in Double value) where TSi : ISi;
    T CreateOther<TOther>(in Double value) where TOther : IOther;
}

internal interface ICreateLinear<out T>
{
    T CreateSi<TSi>(in Double value) where TSi : ISi, ILinear;
    T CreateOther<TOther>(in Double value) where TOther : IOther, ILinear;
}

internal interface IInjectLinear
{
    static abstract T Inject<T>(in ICreateLinear<T> create, in Double value);
}