using Atmoos.Quantities.Dimensions;
using Xunit.Sdk;

namespace Atmoos.Quantities.Test.Dimensions;

// xUnit has a bug that causes it to compare
// types that implement IEnumerable<TSelf> recursively
// such that we get a stack overflow :-(
internal static class DimAssert
{
    public static void Equal(Dimension expected, Dimension actual)
    {
        if (expected.Equals(actual))
        {
            return;
        }
        throw EqualException.ForMismatchedValues(expected, actual);
    }
    public static void NotEqual(Dimension expected, Dimension actual)
    {
        if (expected.Equals(actual))
        {
            var actualValue = actual.ToString() ?? "empty actual";
            var expectedValue = expected.ToString() ?? "empty expected";
            throw NotEqualException.ForEqualValues(expectedValue, actualValue);
        }
    }
    public static void Equal(IEnumerable<Dimension> expected, IEnumerable<Dimension> actual)
    {
        var expectedSet = new HashSet<Dimension>(expected);
        if (expectedSet.SetEquals(actual))
        {
            return;
        }
        Assert.Equal(expected.Select(e => e.ToString()), actual.Select(a => a.ToString()));
    }
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
