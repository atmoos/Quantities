using System.Numerics;

namespace Quantities;

public interface IQuantity<TSelf> : IEquatable<TSelf>, IFormattable
    , IEqualityOperators<TSelf, TSelf, Boolean>
    , IAdditionOperators<TSelf, TSelf, TSelf>
    , ISubtractionOperators<TSelf, TSelf, TSelf>
    , IMultiplyOperators<TSelf, Double, TSelf>
    , IDivisionOperators<TSelf, TSelf, Double>
    where TSelf : struct, IQuantity<TSelf>, Dimensions.IDimension
{
    static abstract implicit operator Double(TSelf self);
    static abstract TSelf operator *(Double scalar, TSelf right);
    static abstract TSelf operator /(TSelf self, Double scalar);
}
