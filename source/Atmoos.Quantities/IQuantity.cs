using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities;

public interface IQuantity<TSelf> : IEquatable<TSelf>, IFormattable, IFactory<TSelf>
    /* ToDo: re-introduce when C# supports roles.
    , IComparisonOperators<TSelf, TSelf, Boolean>
    , IEqualityOperators<TSelf, TSelf, Boolean>
    , IAdditionOperators<TSelf, TSelf, TSelf>
    , ISubtractionOperators<TSelf, TSelf, TSelf>
    , IMultiplyOperators<TSelf, Double, TSelf>
    , IDivisionOperators<TSelf, TSelf, Double> */
    where TSelf : struct, IQuantity<TSelf>, IDimension
{
    internal Quantity Value { get; }
}
