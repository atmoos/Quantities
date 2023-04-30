using Quantities.Measures;
using Quantities.Prefixes;

namespace Quantities.Quantities.Roots;

// ToDo: Use the One & Zero properties to implement multiplicative and additive identities
internal interface IRoot : IPrefixInject<Quant>
{
    public static abstract Quant One { get; }
    public static abstract Quant Zero { get; }
}
