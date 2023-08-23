using Quantities.Numerics;

namespace Quantities.Prefixes;

internal static class Inject<TPrefix>
    where TPrefix : IPrefix
{
    private static readonly Polynomial conversion = Polynomial.Of<TPrefix>();
    public static T Into<T>(in IPrefixInject<T> injector, in Double value)
    {
        return injector.Inject<TPrefix>(conversion.Inverse(in value));
    }
}
