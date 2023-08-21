namespace Quantities.Dimensions;

public interface IDimension
{
    internal static abstract Boolean Is<TDimension>() where TDimension : IDimension;
}

public interface ILinear<TSelf> : IDimension
    where TSelf : ILinear<TSelf>
{
    static Boolean IDimension.Is<TDimension>() => typeof(TDimension).IsAssignableTo(typeof(TSelf));
}
