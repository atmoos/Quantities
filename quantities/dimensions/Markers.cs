namespace Quantities.Dimensions;

public interface ILinear
{ /* marker interface */ }

public interface ISquare<out TLinear> : IProduct<TLinear, TLinear>
{ /* marker interface */ }

public interface ICubic<out TLinear> : IProduct<ISquare<TLinear>, TLinear>
{ /* marker interface */ }

public interface IProduct<out TLeft, out TRight>
{ /* marker interface */ }

public interface IQuotient<out TNominator, out TDenominator>
{ /* marker interface */ }
