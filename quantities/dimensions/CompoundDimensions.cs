namespace Quantities.Dimensions;

public interface IArea : ISquare<ILength>, IDimension { /* marker interface */ }
public interface IVolume : ICubic<ILength>, IDimension { /* marker interface */ }
public interface IVelocity : IPer<ILength, ITime>, IDimension { /* marker interface */ }
public interface IForce : IDimension, ILinear { /* marker interface */ }
public interface IPower : IDimension, ILinear { /* marker interface */ }
public interface IEnergy : ITimes<IPower, ITime>, IDimension { /* marker interface */ }
