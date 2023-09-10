namespace Quantities.Dimensions;

public interface IDimension
{
    internal static abstract Rank RankOf<TDimension>() where TDimension : IDimension;
}
public interface ILinear { /* marker interface */ }

public interface ILinear<TSelf> : ILinear, IDimension
    where TSelf : ILinear<TSelf>
{
    static Rank IDimension.RankOf<TDimension>() => typeof(TDimension).IsAssignableTo(typeof(TSelf)) ? Rank.Linear : Rank.None;
}
