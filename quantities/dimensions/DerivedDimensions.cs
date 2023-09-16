namespace Quantities.Dimensions;

public interface IArea : ISquare<IArea, ILength>, IDerivedQuantity { /* marker interface */ }
public interface IVolume : ICubic<IVolume, ILength>, IDerivedQuantity { /* marker interface */ }
public interface IVelocity : IQuotient<IVelocity, ILength, ITime>, IDerivedQuantity { /* marker interface */ }
// Dim<Length>.Times<Mass>() * Dim<Time>.Pow(-2)
public interface IForce : IQuotient<IForce, IProduct<ILength, IMass>, ISquare<ITime>>, IDerivedQuantity { /* marker interface */ }
public interface IPower : ILinear<IPower>, IDerivedQuantity { /* marker interface */ }
public interface IEnergy : IProduct<IEnergy, IPower, ITime>, IDerivedQuantity { /* marker interface */ }
public interface IAngle : IQuotient<IAngle, ILength, ILength>, IDerivedQuantity { /* marker interface */ }
