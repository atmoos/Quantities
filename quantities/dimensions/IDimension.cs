﻿namespace Quantities.Dimensions;

// ToDo: rethink the utility of these dimensions!
public interface IDimension { /* marker interface */ }

public interface ILinear { /* marker interface */ }

public interface ISquare<out TDimension>
where TDimension : IDimension
{
}
public interface ICubic<out TDimension>
    where TDimension : IDimension
{
}
