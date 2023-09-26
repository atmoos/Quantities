using Quantities.Core.Numerics;

namespace Quantities.Prefixes;

internal static class Build<TPrefix>
    where TPrefix : IPrefix
{
    private static readonly Polynomial conversion = Polynomial.Of<TPrefix>();
    public static T Scaled<T>(in IPrefixInject<T> injector, in Double value)
    {
        return injector.Inject<TPrefix>(conversion / value);
    }
}
