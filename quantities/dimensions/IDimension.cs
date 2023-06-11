namespace Quantities.Dimensions;

// ToDo: rethink the utility of these dimensions!
public interface IDimension { /* marker interface */ }

public interface ILinear { /* marker interface */ }
public interface ISquare { /* marker interface */ }
public interface ICubic { /* marker interface */ }

public interface ISquare<out TDimension> : ITimes<TDimension, TDimension>, ISquare
where TDimension : IDimension
{
}
public interface ICubic<out TDimension> : ITimes<ISquare<TDimension>, TDimension>, ICubic
    where TDimension : IDimension
{
}
