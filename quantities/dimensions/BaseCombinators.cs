namespace Quantities.Dimensions;

public interface IProduct<out TLeft, out TRight>
    where TLeft : IDimension
    where TRight : IDimension
{ /* marker interface */ }

public interface IQuotient<out TNominator, out TDenominator>
    where TNominator : IDimension
    where TDenominator : IDimension
{ /* marker interface */ }
