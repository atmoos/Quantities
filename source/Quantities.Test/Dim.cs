using Quantities.Dimensions;

namespace Quantities.Test;

internal static class Dim<TSelf>
    where TSelf : IDimension
{
    public static Dimension Value => TSelf.D;
    public static Dimension Pow(Int32 n) => TSelf.D.Pow(n);
    public static Dimension Times<TOther>()
        where TOther : IDimension => TSelf.D * TOther.D;
    public static Dimension Per<TOther>()
        where TOther : IDimension => TSelf.D / TOther.D;
}
