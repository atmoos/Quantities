using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units.Si;
using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Test.Measures;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class MeasuresPhase1Branches
{
    private const String zeroWidthNonJoiner = "\u200C";

    [Theory]
    [InlineData(2, "m⁻²")]
    [InlineData(-2, "s⁻²")]
    public void InvertiblePowerCoversBothExponentSignBranches(Int32 exponent, String expected)
    {
        String actual = CaptureRepresentation<Invertible<Si<Metre>, Si<Second>>>(exponent);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ProductRepresentationUsesLeftOverInverseRightBranch()
    {
        Assert.Equal("m/s", Product<Si<Metre>, Power<Si<Second>, Negative<One>>>.Representation);
    }

    [Fact]
    public void ProductRepresentationUsesRightOverInverseLeftBranch()
    {
        Assert.Equal("s/m", Product<Power<Si<Metre>, Negative<One>>, Si<Second>>.Representation);
    }

    [Fact]
    public void ProductRepresentationUsesJoinerBranchWhenNoDivisionIsNeeded()
    {
        Assert.Equal($"m{zeroWidthNonJoiner}s", Product<Si<Metre>, Si<Second>>.Representation);
    }

    [Theory]
    [InlineData(1, "h")]
    [InlineData(2, "h²")]
    public void AliasPowerCoversAliasEqualityAndRaisedLinearFallback(Int32 exponent, String expected)
    {
        String actual = CaptureRepresentation<Alias<Metric<Hour>, Metric<Hour>>>(exponent);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(3, "m³")]
    [InlineData(0, "𝟙")]
    [InlineData(-2, "m⁻²")]
    public void RaiseCoversPositiveZeroAndNegativeBranches(Int32 exponent, String expected)
    {
        String actual = CaptureRepresentation<Si<Metre>>(exponent);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RaiseThrowsForUnsupportedExponent()
    {
        Assert.Throws<InvalidOperationException>(() => CaptureRepresentation<Si<Metre>>(6));
    }

    [Fact]
    public void InverseOfNegativePowerOneReturnsLinearMeasure()
    {
        Expect<Si<Metre>>.ToBeInverseOf<Power<Si<Metre>, Negative<One>>>("m");
    }

    [Fact]
    public void InverseOfNegativePowerTwoReturnsPositivePowerMeasure()
    {
        Expect<Power<Si<Metre>, Two>>.ToBeInverseOf<Power<Si<Metre>, Negative<Two>>>("m²");
    }

    private static String CaptureRepresentation<TMeasure>(Int32 exponent)
        where TMeasure : IMeasure
    {
        RepresentationVisitor visitor = new();

        TMeasure.Power(visitor, exponent);

        return visitor.Result;
    }

    private sealed class RepresentationVisitor : IVisitor
    {
        public String Result { get; private set; } = String.Empty;

        public Result? Build(Polynomial poly, Dimension target)
        {
            throw new NotSupportedException("Build is not needed for these tests.");
        }

        public IVisitor Inject<TMeasure>()
            where TMeasure : IMeasure
        {
            this.Result = TMeasure.Representation;
            return this;
        }
    }
}
