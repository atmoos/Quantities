namespace Quantities.Dimensions;

public interface IDimension { /* marker interface */ }

public interface ISquare<out TDimension> : ITimes<TDimension, TDimension>
where TDimension : IDimension
{
}
public interface ICubic<out TDimension> : ITimes<ISquare<TDimension>, TDimension>
    where TDimension : IDimension
{
}
