namespace Quantities.Dimensions;

public interface IProduct<out TLeft, out TRight>
    where TLeft : IDimension
    where TRight : IDimension
{ /* marker interface */ }

public interface IFraction<out TNominator, out TDenominator>
    where TNominator : IDimension
    where TDenominator : IDimension
{ /* marker interface */ }
