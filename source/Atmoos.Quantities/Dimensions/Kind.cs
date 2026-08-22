

using System.Numerics;
using System.Runtime.CompilerServices;

namespace Atmoos.Quantities.Dimensions;

internal abstract class Kind : IEquatable<Kind>,
        IEqualityOperators<Kind, Kind, Boolean>
{
    private Kind() { }

    public static ref readonly Kind Of<TDimension>()
        where TDimension : IDimension => ref AllocationFree<Kind, Impl<TDimension>>.Item;

    public override Boolean Equals(Object? other) => other is Kind k && ReferenceEquals(this, k);
    public Boolean Equals(Kind? other) => other is Kind k && ReferenceEquals(this, k);
    public override Int32 GetHashCode() => RuntimeHelpers.GetHashCode(this);

    public static Boolean operator ==(Kind? left, Kind? right) => left is Kind l && l.Equals(right);

    public static Boolean operator !=(Kind? left, Kind? right) => !(left == right);

    internal sealed class Impl<TDimension> : Kind
        where TDimension : IDimension;
}
