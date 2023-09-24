namespace Quantities.Dimensions;

public interface IDimension
{
    internal static abstract Dim D { get; }
}
public interface ILinear<TSelf> : ILinear, IDimension
    where TSelf : ILinear<TSelf>
{
    static Dim IDimension.D => Scalar.Of<TSelf>();
}
