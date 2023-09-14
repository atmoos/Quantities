namespace Quantities.Dimensions;

public interface IArea : ISquare<IArea, ILength> { /* marker interface */ }
public interface IVolume : ICubic<IVolume, ILength> { /* marker interface */ }
public interface IVelocity : IQuotient<IVelocity, ILength, ITime> { /* marker interface */ }
public interface IForce : ILinear<IForce> { /* marker interface */ }
public interface IPower : ILinear<IPower> { /* marker interface */ }
public interface IEnergy : IProduct<IEnergy, IPower, ITime> { /* marker interface */ }
public interface IAngle : IQuotient<IAngle, ILength, ILength> { /* marker interface */ }
