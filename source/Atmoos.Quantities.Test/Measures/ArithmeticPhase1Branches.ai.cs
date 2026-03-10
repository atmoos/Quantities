using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units.Si;

namespace Atmoos.Quantities.Test.Measures;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class ArithmeticPhase1Branches
{
    [Fact]
    public void MapReturnsIdentityForUnitTarget()
    {
        Result actual = Assert.IsType<Result>(Arithmetic<Si<Metre>>.Map<Si<Second>>(Polynomial.One, Unit.Identity));
        Measure measure = actual;

        Assert.Same(Measure.Of<Identity>(), measure);
    }

    [Fact]
    public void MapReturnsMatchingLinearTargetWithoutFallback()
    {
        Result actual = Assert.IsType<Result>(Arithmetic<Si<Metre>>.Map<Identity>(Polynomial.One, Scalar.Of<ILength>()));
        Measure measure = actual;

        Assert.Same(Measure.Of<Si<Metre>>(), measure);
    }

    [Fact]
    public void MapFallbackCanBuildByDirectFactoryMatch()
    {
        Result actual = Assert.IsType<Result>(Arithmetic<Invertible<Product<Si<Metre>, Si<Second>>, Identity>>.Map<Identity>(Polynomial.One, Invertible<Product<Si<Metre>, Si<Second>>, Identity>.D));
        Measure measure = actual;

        Assert.NotNull(measure);
    }

    [Fact]
    public void MapFallbackCanBuildByChainingLeftAndRightFactories()
    {
        Dimension target = Invertible<Product<Si<Metre>, Si<Second>>, Identity>.D * Invertible<Product<Si<Ampere>, Si<Kelvin>>, Identity>.D;
        Result actual = Assert.IsType<Result>(Arithmetic<Invertible<Product<Si<Metre>, Si<Second>>, Identity>>.Map<Invertible<Product<Si<Ampere>, Si<Kelvin>>, Identity>>(Polynomial.One, target));
        Measure measure = actual;

        Assert.NotNull(measure);
    }

    [Fact]
    public void MapReturnsNullWhenNoFallbackPathMatches()
    {
        Result? actual = Arithmetic<Si<Metre>>.Map<Si<Second>>(Polynomial.One, Scalar.Of<IMass>());

        Assert.Null(actual);
    }

    [Fact]
    public void InvertCoversZeroZeroBranch()
    {
        AssertInvert<Identity, Identity, Identity>();
    }

    [Fact]
    public void InvertCoversNegativePositiveBranch()
    {
        AssertInvert<Si<Metre>, Power<Si<Second>, Negative<One>>, Product<Si<Second>, Power<Si<Metre>, Negative<One>>>>();
    }

    [Fact]
    public void InvertCoversZeroOtherBranch()
    {
        AssertInvert<Identity, Si<Second>, Power<Si<Second>, Negative<One>>>();
    }

    [Fact]
    public void InvertCoversOtherZeroBranch()
    {
        AssertInvert<Si<Metre>, Identity, Power<Si<Metre>, Negative<One>>>();
    }

    [Fact]
    public void InvertCoversDefaultProductBranch()
    {
        AssertInvert<Power<Si<Metre>, Negative<One>>, Si<Second>, Product<Si<Metre>, Power<Si<Second>, Negative<One>>>>();
    }

    private static void AssertInvert<TLeft, TRight, TExpected>()
        where TLeft : IMeasure
        where TRight : IMeasure
        where TExpected : IMeasure
    {
        Measure actual = Arithmetic<TLeft>.Invert<Measure, TRight>(new MeasureInjector());

        Assert.Same(Measure.Of<TExpected>(), actual);
    }

    private sealed class MeasureInjector : IInject<Measure>
    {
        public Measure Inject<TMeasure>()
            where TMeasure : IMeasure => Measure.Of<TMeasure>();
    }
}
