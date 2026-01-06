using Atmoos.Quantities.Core.Numerics;

namespace Atmoos.Quantities.Dimensions;

public interface IDimension
{
    internal static abstract Dimension D { get; }
}

public interface IDimension<TSelf, TMultiplicity> : IDimension
    where TSelf : IDimension
    where TMultiplicity : INumber
{
    static Dimension IDimension.D { get; } = Scalar.Of<TSelf>(TMultiplicity.Value);
}

public interface ILinear<TSelf> : ILinear, IDimension<TSelf, One>
    where TSelf : ILinear<TSelf>
{ }
