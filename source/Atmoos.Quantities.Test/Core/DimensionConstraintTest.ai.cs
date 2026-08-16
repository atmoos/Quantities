using Atmoos.Quantities.Core;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Test.Dimensions;

namespace Atmoos.Quantities.Test.Core;

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public sealed class DimensionConstraintTest
{
    [Fact]
    public void DimensionContractExposesItsDefinition()
    {
        AssertDimension<ILength>(Dim<ILength>.Value);
        AssertDimension<IVelocity>(Dim<ILength>.Per<ITime>());
    }

    [Fact]
    public void ProductContractPreservesItsOperandDimensions()
    {
        AssertProduct<Momentum, IMomentum, IMass, IVelocity>(Dim<IMass>.Value * Dim<IVelocity>.Value);
        AssertProduct<Energy, IEnergy, IPower, ITime>(Dim<IPower>.Value * Dim<ITime>.Value);
    }

    [Fact]
    public void QuotientContractPreservesItsOperandDimensions()
    {
        AssertQuotient<Velocity, IVelocity, ILength, ITime>(Dim<ILength>.Per<ITime>());
        AssertQuotient<Acceleration, IAcceleration, ILength, ITime, Two>(Dim<ILength>.Value / Dim<ITime>.Value.Pow(2));
    }

    private static void AssertDimension<TDimension>(Dimension expected)
        where TDimension : IDimension
    {
        DimAssert.Equal(expected, TDimension.D);
    }

    private static void AssertProduct<TQuantity, TDimension, TLeftDimension, TRightDimension>(Dimension expected)
        where TQuantity : IProduct<TQuantity, TDimension, TLeftDimension, TRightDimension>, TDimension
        where TDimension : IProduct<TLeftDimension, TRightDimension>, IMultiplicity<TDimension, One>
        where TLeftDimension : IDimension
        where TRightDimension : IDimension
    {
        DimAssert.Equal(expected, TDimension.D);
    }

    private static void AssertQuotient<TQuantity, TDimension, TNominatorDimension, TDenominatorDimension, TExponent>(Dimension expected)
        where TQuantity : IQuotient<TQuantity, TDimension, TNominatorDimension, TDenominatorDimension, TExponent>, TDimension
        where TDimension : IProduct<TNominatorDimension, IDimension<TDenominatorDimension, Negative<TExponent>>>, IMultiplicity<TDimension, One>
        where TNominatorDimension : IMultiplicity<TNominatorDimension, One>, IDimension
        where TDenominatorDimension : IMultiplicity<TDenominatorDimension, One>, IDimension
        where TExponent : INumber, IPositive
    {
        DimAssert.Equal(expected, TDimension.D);
    }

    private static void AssertQuotient<TQuantity, TDimension, TNominatorDimension, TDenominatorDimension>(Dimension expected)
        where TQuantity : IQuotient<TQuantity, TDimension, TNominatorDimension, TDenominatorDimension>, TDimension
        where TDimension : IProduct<TNominatorDimension, IDimension<TDenominatorDimension, Negative<One>>>, IMultiplicity<TDimension, One>
        where TNominatorDimension : IMultiplicity<TNominatorDimension, One>, IDimension
        where TDenominatorDimension : IMultiplicity<TDenominatorDimension, One>, IDimension
    {
        DimAssert.Equal(expected, TDimension.D);
    }
}
