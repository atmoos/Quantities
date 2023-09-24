using System.Diagnostics.CodeAnalysis;
using Quantities.Dimensions;

namespace Quantities.Test.Dimensions;

internal sealed class DimComparer : IEqualityComparer<Dim>
{
    public Boolean Equals(Dim? x, Dim? y) => x?.Equals(y) ?? y == null;
    public Int32 GetHashCode([DisallowNull] Dim obj) => obj.GetHashCode();
}

// These are alle "dummy" classes.
// They are used to test "RankOf" on these interfaces:
// ILinear<TS>, ISquare<TS,T>, ICubic<TS,T>
// IProduct<TS,TV>, IQuotient<TS,N,D>
// where TS stands for TSelf.
internal sealed class Time : ITime { }
internal sealed class OtherTime : ITime { }
internal sealed class Length : ILength { }
internal sealed class OtherLength : ILength { }
internal sealed class Area : IArea { }
internal sealed class OtherArea : IArea { }
internal sealed class Volume : IVolume { }
internal sealed class OtherVolume : IVolume { }
internal sealed class Temperature : ITemperature { }
internal sealed class SquareTemperature : ISquareTemperature { }
internal sealed class CubicMass : ICubicMass { }
internal sealed class Velocity : IVelocity { }
internal sealed class OtherVelocity : IVelocity { }
internal sealed class Angle : IAngle { }
internal sealed class Current : IElectricCurrent { }
internal sealed class Ampere : IElectricCurrent { }
internal sealed class Coulomb : IElectricCharge { }
internal sealed class AmpereHour : IElectricCharge { }
internal sealed class DoubleTime : IDoubleTime { }
internal interface ICubicMass : ICubic<IMass> { }
internal interface IDoubleTime : IProduct<ITime, ITime> { }
internal interface ISquareTemperature : ISquare<ITemperature> { }
