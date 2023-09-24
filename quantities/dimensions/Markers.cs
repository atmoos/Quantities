﻿namespace Quantities.Dimensions;

public interface IBaseQuantity
{ /* marker interface */ }

public interface IDerivedQuantity
{ /* marker interface */ }

public interface ILinear
{ /* marker interface */ }

public interface ISquare<TLinear> : IDimension
    where TLinear : IDimension
{
    static Dim IDimension.D => TLinear.D.Pow(2);
}

public interface ICubic<out TLinear> : IDimension
    where TLinear : IDimension
{
    static Dim IDimension.D => TLinear.D.Pow(3);
}

public interface IProduct<TLeft, TRight> : IDimension
    where TLeft : IDimension
    where TRight : IDimension
{
    static Dim IDimension.D => TLeft.D * TRight.D;
}

public interface IQuotient<out TNominator, out TDenominator> : IDimension
    where TNominator : IDimension
    where TDenominator : IDimension
{
    static Dim IDimension.D => TNominator.D / TDenominator.D;
}
