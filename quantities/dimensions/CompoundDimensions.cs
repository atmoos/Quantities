﻿namespace Quantities.Dimensions;

public interface IArea<out TLength> : ISquare<TLength>
    where TLength : ILength
{ /* marker interface */ }
public interface IVolume<out TLength> : ICubic<TLength>
    where TLength : ILength
{ /* marker interface */ }
public interface IVelocity : IDimension { /* marker interface */ }
public interface IVelocity<out TLength, out TTime> : IPer<TLength, TTime>, IVelocity
    where TLength : ILength
    where TTime : ITime
{ /* marker interface */ }
public interface IPower : IDimension { /* marker interface */ }
public interface IPower<out TMass, out TLength, out TTime> : IPer<ITimes<TMass, ISquare<TLength>>, ICubic<TTime>>, IPower
    where TMass : IMass
    where TLength : ILength
    where TTime : ITime
{ /* marker interface */ }
