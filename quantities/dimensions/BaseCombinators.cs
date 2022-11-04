namespace Quantities.Dimensions;

public interface ITimes<out TLeft, out TRight> : IDimension
    where TLeft : IDimension
    where TRight : IDimension
{ /* marker interface */ }
public interface IPer<out TNominator, out TDenominator> : IDimension
    where TNominator : IDimension
    where TDenominator : IDimension
{ /* marker interface */ }
