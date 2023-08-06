using Quantities.Numerics;
using static Quantities.Extensions;

namespace Quantities.Prefixes;

internal static class Build<TPrefix>
    where TPrefix : IPrefix
{
    private static readonly Polynomial conversion = PolynomialOf<TPrefix>();
    public static T Scaled<T>(in IPrefixInject<T> injector, in Double value)
    {
        return injector.Inject<TPrefix>(conversion.Inverse(in value));
    }
}
