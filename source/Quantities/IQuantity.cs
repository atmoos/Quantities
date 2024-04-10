using System.Numerics;
using Quantities.Dimensions;

namespace Quantities;

public interface IQuantity<TSelf> : IEquatable<TSelf>, IFormattable
    , IFactory<TSelf>
    , ICastOperators<TSelf, Double>
    , IComparisonOperators<TSelf, TSelf, Boolean>
    , IEqualityOperators<TSelf, TSelf, Boolean>
    , IAdditionOperators<TSelf, TSelf, TSelf>
    , ISubtractionOperators<TSelf, TSelf, TSelf>
    , IMultiplyOperators<TSelf, Double, TSelf>
    , IDivisionOperators<TSelf, TSelf, Double>
    where TSelf : struct, IQuantity<TSelf>, IDimension
{
    internal Quantity Value { get; }
    static abstract TSelf operator *(Double scalar, TSelf right);
    static abstract TSelf operator /(TSelf self, Double scalar);
}
