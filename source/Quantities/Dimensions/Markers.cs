namespace Quantities.Dimensions;

public interface IBaseQuantity
{ /* marker interface */ }

public interface IDerivedQuantity
{ /* marker interface */ }

public interface ILinear
{ /* marker interface */ }

public interface ISquare<out TLinear> : IDimension
    where TLinear : IDimension
{
    static Dimension IDimension.D => TLinear.D.Pow(2);
}

public interface ICubic<out TLinear> : IDimension
    where TLinear : IDimension
{
    static Dimension IDimension.D => TLinear.D.Pow(3);
}

public interface IProduct<out TLeft, out TRight> : IDimension
    where TLeft : IDimension
    where TRight : IDimension
{
    static Dimension IDimension.D => TLeft.D * TRight.D;
}

public interface IQuotient<out TNominator, out TDenominator> : IDimension
    where TNominator : IDimension
    where TDenominator : IDimension
{
    static Dimension IDimension.D => TNominator.D / TDenominator.D;
}

public interface IInverse<out TBase> : IDimension
    where TBase : IDimension
{
    static Dimension IDimension.D => TBase.D.Pow(-1);
}
