﻿using static Quantities.Dimensions.Rank;

namespace Quantities.Dimensions;

public interface IDimension
{
    internal static abstract Rank RankOf<TDimension>() where TDimension : IDimension;
}

public interface ILinear<TSelf> : ILinear, IDimension
    where TSelf : ILinear<TSelf>
{
    static Rank IDimension.RankOf<TDimension>() => Rank<TSelf>.Of<TDimension>();
}

public interface ISquare<TSelf, out TLinear> : ISquare<TLinear>, ILinear<TSelf>
    where TSelf : ISquare<TSelf, TLinear>
    where TLinear : ILinear<TLinear>
{
    static Rank IDimension.RankOf<TDimension>()
        => TLinear.RankOf<TDimension>() == One ? Two : Rank<TSelf>.Of<TDimension>();
}

public interface ICubic<TSelf, out TLinear> : ICubic<TLinear>, ILinear<TSelf>
    where TSelf : ICubic<TSelf, TLinear>
    where TLinear : ILinear<TLinear>
{
    static Rank IDimension.RankOf<TDimension>()
        => TLinear.RankOf<TDimension>() == One ? Three : Rank<TSelf>.Of<TDimension>();
}

public interface IProduct<TSelf, TLeft, TRight> : IProduct<TLeft, TRight>, ILinear<TSelf>
    where TSelf : IProduct<TSelf, TLeft, TRight>
    where TLeft : ILinear<TLeft>
    where TRight : ILinear<TRight>
{
    static Rank IDimension.RankOf<TDimension>()
    {
        Rank left = TLeft.RankOf<TDimension>();
        Rank right = TRight.RankOf<TDimension>();
        return (left, right) switch {
            (One, One) => Two,
            (One, _) => HigherOrder,
            (_, One) => HigherOrder,
            _ => Rank<TSelf>.Of<TDimension>()
        };
    }
}
public interface IQuotient<TSelf, TNominator, TDenominator> : IQuotient<TNominator, TDenominator>, ILinear<TSelf>
    where TSelf : IQuotient<TSelf, TNominator, TDenominator>
    where TNominator : ILinear<TNominator>
    where TDenominator : ILinear<TDenominator>
{
    static Rank IDimension.RankOf<TDimension>()
    {
        Rank nominator = TNominator.RankOf<TDimension>();
        Rank denominator = TDenominator.RankOf<TDimension>();
        return (nominator, denominator) switch {
            (One, One) => Zero,
            (One, _) => HigherOrder,
            (_, One) => HigherOrder,
            _ => Rank<TSelf>.Of<TDimension>()
        };
    }
}

file static class Rank<TSelf>
    where TSelf : IDimension
{
    public static Rank Of<TOther>() where TOther : IDimension
        => typeof(TOther).IsAssignableTo(typeof(TSelf)) ? One : None;
}
