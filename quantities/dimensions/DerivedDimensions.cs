namespace Quantities.Dimensions;

public interface IArea : ISquare<ILength>, ILinear<IArea> { /* marker interface */ }
public interface IVolume : ICubic<ILength>, ILinear<IVolume> { /* marker interface */ }
public interface IVelocity : IQuotient<ILength, ITime>, ILinear<IVelocity> { /* marker interface */ }
public interface IForce : ILinear<IForce> { /* marker interface */ }
public interface IPower : ILinear<IPower> { /* marker interface */ }
public interface IEnergy : IProduct<IPower, ITime>, ILinear<IEnergy> { /* marker interface */ }
