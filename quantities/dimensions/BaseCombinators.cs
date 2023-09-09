namespace Quantities.Dimensions;

public interface IProduct<out TLeft, out TRight>
    where TLeft : IDimension
    where TRight : IDimension
{ /* marker interface */ }

public interface IQuotient<out TNominator, out TDenominator>
    where TNominator : IDimension
    where TDenominator : IDimension
{ /* marker interface */ }

public interface ISquare<out TDimension> : IProduct<TDimension, TDimension>
    where TDimension : IDimension
{ /* marker interface */ }

public interface ICubic<out TDimension>
    where TDimension : IDimension
{ /* marker interface */ }
