namespace Atmoos.Quantities.Dimensions;

public interface IDimension
{
    internal static abstract Dimension D { get; }
}
public interface ILinear<TSelf> : ILinear, IDimension
    where TSelf : ILinear<TSelf>
{
    static Dimension IDimension.D => Scalar.Of<TSelf>();
}
