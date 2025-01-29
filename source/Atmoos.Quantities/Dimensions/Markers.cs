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
