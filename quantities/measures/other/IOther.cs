
namespace Quantities.Measures.Other;

internal interface IOther : IInjectLinear, IRepresentable
{
    static abstract Double ToSi(in Double nonSiValue);
    static abstract Double FromSi(in Double siValue);
}