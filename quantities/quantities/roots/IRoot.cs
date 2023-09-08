using Quantities.Measures;
using Quantities.Prefixes;

namespace Quantities.Quantities.Roots;

// ToDo: Use the One & Zero properties to implement multiplicative and additive identities
internal interface IRoot : IPrefixInject<Quantity>
{
    public static abstract Quantity One { get; }
    public static abstract Quantity Zero { get; }
}
