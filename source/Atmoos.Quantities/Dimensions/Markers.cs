namespace Atmoos.Quantities.Dimensions;

public interface IBaseQuantity
{ /* marker interface */ }

public interface IDerivedQuantity
{ /* marker interface */ }

public interface ILinear
{ /* marker interface */ }

public interface IProduct<out TLeft, out TRight> : IDimension
    where TLeft : IDimension
    where TRight : IDimension
{
    static Dimension IDimension.D => TLeft.D * TRight.D;
}

// ToDo: Remove this interface
public interface IQuotient<out TNominator, out TDenominator> : IDimension
    where TNominator : IDimension
    where TDenominator : IDimension
{
    static Dimension IDimension.D => TNominator.D / TDenominator.D;
}

// ToDo: Remove this interface
public interface IInverse<out TBase> : IDimension
    where TBase : IDimension
{
    static Dimension IDimension.D => TBase.D.Pow(-1);
}
