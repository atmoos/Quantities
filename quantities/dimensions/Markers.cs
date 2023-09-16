namespace Quantities.Dimensions;

public interface IBaseQuantity
{ /* marker interface */ }
public interface IHigherOrder<out TLinear>
{ /* marker interface */ }

public interface IDerivedQuantity
{ /* marker interface */ }

public interface ILinear
{ /* marker interface */ }

public interface ISquare<TLinear> : ILinear<ISquare<TLinear>>, IDimension, IHigherOrder<TLinear>
    where TLinear : IDimension
{
    static Dim IDimension.D => TLinear.D.Pow(2);
    static Rank IDimension.RankOf<TDimension>() => Rank.None;
}

public interface ICubic<out TLinear> : IDimension, IHigherOrder<TLinear>
    where TLinear : IDimension
{
    static Dim IDimension.D => TLinear.D.Pow(3);
    static Rank IDimension.RankOf<TDimension>() => Rank.None;
}

public interface IProduct<TLeft, TRight> : ILinear<IProduct<TLeft, TRight>>, IDimension
    where TLeft : IDimension
    where TRight : IDimension
{
    static Dim IDimension.D => TLeft.D * TRight.D;
    static Rank IDimension.RankOf<TDimension>() => Rank.None;
}

public interface IQuotient<out TNominator, out TDenominator> : IDimension
    where TNominator : IDimension
    where TDenominator : IDimension
{
    static Dim IDimension.D => TNominator.D / TDenominator.D;
    static Rank IDimension.RankOf<TDimension>() => Rank.None;
}
