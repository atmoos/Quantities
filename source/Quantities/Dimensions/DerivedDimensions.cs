namespace Quantities.Dimensions;

public interface IArea : ISquare<ILength>, IDerivedQuantity { /* marker interface */ }
public interface IVolume : ICubic<ILength>, IDerivedQuantity { /* marker interface */ }
public interface IVelocity : IQuotient<ILength, ITime>, IDerivedQuantity { /* marker interface */ }
public interface IForce : IQuotient<IProduct<ILength, IMass>, ISquare<ITime>>, IDerivedQuantity { /* marker interface */ }
public interface IPower : ILinear<IPower>, IDerivedQuantity { /* marker interface */ }
public interface IEnergy : IProduct<IPower, ITime>, IDerivedQuantity { /* marker interface */ }
public interface IAngle : IQuotient<ILength, ILength>, IDerivedQuantity { /* marker interface */ }
public interface IFrequency : IInverse<ITime>, IDerivedQuantity { /* marker interface */ }
