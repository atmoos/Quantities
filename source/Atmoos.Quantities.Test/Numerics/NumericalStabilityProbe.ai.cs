using Atmoos.Quantities.Core.Numerics;
using Xunit.Abstractions;
using static System.Math;
using static Atmoos.Quantities.Test.Convenience;
using static Atmoos.Quantities.TestTools.Convenience;

namespace Atmoos.Quantities.Test.Numerics;

// Counterexamples: scenarios where naive double arithmetic is more
// numerically stable than the library's Polynomial layer.
//
// Root cause: Polynomial.Pow(N) with nonzero offset composes the transform
// N times symbolically. When the scale factor is > 1, each composition
// multiplies the offset by the scale, causing it to grow exponentially.
// A 64-bit double storing that huge offset loses relative precision in
// its fractional digits. When the inverse Pow(-N) is then composed, the
// offsets must cancel — but they've already been rounded, leaving a
// residual. Sequential step-by-step evaluation avoids this because the
// offset stays small (just the constant 'b') at every step.
[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public sealed class NumericalStabilityProbe(ITestOutputHelper output)
{
    // Counterexample 1: Upscaling power with exact integer coefficients.
    //
    // p(x) = 10x + 100. Sequential evaluation: each step is
    // 10*x + 100 (exact for any x < 2^50) and (x-100)/10 (one rounding).
    // Polynomial: Pow(N) builds offset ≈ 100*(10^N - 1)/9, which at N=15
    // is ~1.11e16 — so large that its fractional part vanishes in a Double.
    // When Pow(-15) is composed, the offset residual is ~0.06, i.e. ~2%
    // of the input value 1.5, which is a catastrophic error.
    [Theory]
    [InlineData(5, 6.5370e-11)]
    [InlineData(10, 4.8668e-06)]
    [InlineData(15, 6e-02)]
    public void UpscalingPowerWithIntegerCoefficients(Int32 exponent, Double polyErrorBound)
    {
        const Double value = 1.5;
        const Double n = 10d, d = 1d, offset = 100d;

        // Sequential: apply forward N times, then inverse N times.
        Double sequential = value;
        for (Int32 i = 0; i < exponent; i++)
            sequential = n * sequential + offset;
        for (Int32 i = 0; i < exponent; i++)
            sequential = (sequential - offset) / n;
        Double naiveError = Abs(sequential - value);

        // Polynomial: compose p^N * p^{-N}, evaluate once.
        var poly = Poly(n, d, offset);
        var roundTrip = poly.Pow(exponent) * poly.Pow(-exponent);
        Double polyResult = roundTrip * value;
        Double polyError = Abs(polyResult - value);

        output.WriteLine(
            $"exp={exponent}: naive={naiveError:e4}, poly={polyError:e4}, " +
            $"ratio={SafeRatio(polyError, naiveError):f0}x");

        // Sequential achieves EXACT results (integer arithmetic is exact).
        Assert.Equal(0d, naiveError);
        // Polynomial has non-trivial error that grows with exponent.
        Assert.True(polyError <= polyErrorBound,
            $"exp={exponent}: poly error {polyError:e4} exceeds bound {polyErrorBound:e4}");
        Assert.True(polyError > naiveError,
            $"exp={exponent}: expected polynomial to be less precise than naive");
    }

    // Counterexample 2: Upscaling power with non-integer ratio, large offset.
    //
    // p(x) = (7/3)x + 100 (scale > 1). After composing p^7, the offset
    // grows to ~37,000. The offset rounding residual after composing with
    // p^{-7} causes measurable error. Sequential evaluation keeps the
    // per-step offset at a constant 100, accumulating only ~7 ULP.
    [Theory]
    [InlineData(5, 100)]
    [InlineData(7, 100)]
    [InlineData(5, 1000)]
    [InlineData(7, 1000)]
    public void UpscalingPowerWithFractionalRatioAndLargeOffset(Int32 exponent, Double offset)
    {
        const Double value = PI;
        const Double n = 7d, d = 3d;

        Double sequential = value;
        for (Int32 i = 0; i < exponent; i++)
            sequential = n / d * sequential + offset;
        for (Int32 i = 0; i < exponent; i++)
            sequential = d / n * (sequential - offset);
        Double naiveError = Abs(sequential - value);

        var poly = Poly(n, d, offset);
        var roundTrip = poly.Pow(exponent) * poly.Pow(-exponent);
        Double polyResult = roundTrip * value;
        Double polyError = Abs(polyResult - value);

        output.WriteLine(
            $"exp={exponent},o={offset}: naive={naiveError:e4}, poly={polyError:e4}, " +
            $"ratio={SafeRatio(polyError, naiveError):f0}x");

        Assert.True(polyError > naiveError,
            $"exp={exponent},o={offset}: expected polynomial to be less precise");
    }

    // Counterexample 3: Downscaling power with offset.
    //
    // Even with scale < 1 (n/d = 3/2), a large offset still causes the
    // composed polynomial to lose precision relative to sequential eval.
    [Theory]
    [InlineData(3, 50)]
    [InlineData(5, 50)]
    [InlineData(7, 50)]
    [InlineData(5, 1000)]
    [InlineData(7, 1000)]
    public void DownscalingPowerWithOffset(Int32 exponent, Double offset)
    {
        const Double value = PI;
        const Double n = 3d, d = 2d;

        Double sequential = value;
        for (Int32 i = 0; i < exponent; i++)
            sequential = n / d * sequential + offset;
        for (Int32 i = 0; i < exponent; i++)
            sequential = d / n * (sequential - offset);
        Double naiveError = Abs(sequential - value);

        var poly = Poly(n, d, offset);
        var roundTrip = poly.Pow(exponent) * poly.Pow(-exponent);
        Double polyResult = roundTrip * value;
        Double polyError = Abs(polyResult - value);

        output.WriteLine(
            $"exp={exponent},o={offset}: naive={naiveError:e4}, poly={polyError:e4}, " +
            $"ratio={SafeRatio(polyError, naiveError):f0}x");

        Assert.True(polyError > naiveError,
            $"exp={exponent},o={offset}: expected polynomial to be less precise");
    }

    // Counterexample 4: Inverse evaluation near the offset.
    //
    // The polynomial's inverse formula: d*(x - offset)/n.
    // When x ≈ offset, the subtraction x - offset is catastrophic
    // cancellation: most significant bits cancel, leaving only noise.
    // A caller who has access to the small delta directly (i.e. knowing
    // x = offset + delta) can compute d*delta/n without subtraction,
    // getting zero error.
    //
    // This is a structural limitation: the Polynomial only receives x,
    // not the decomposition x = offset + delta.
    [Theory]
    [InlineData(1e-10)]
    [InlineData(1e-12)]
    [InlineData(1e-14)]
    public void InverseEvaluationNearOffset(Double delta)
    {
        const Double n = 5d, d = 3d, offset = 1000d;
        Double x = offset + delta;
        Double expected = d * delta / n;

        // Polynomial inverse: must subtract x - offset, losing bits.
        var poly = Poly(n, d, offset);
        Double polyResult = poly / x;
        Double polyError = Abs(polyResult - expected);

        // "Naive" with structural knowledge: compute from delta directly.
        Double naiveResult = d * delta / n;
        Double naiveError = Abs(naiveResult - expected);

        output.WriteLine(
            $"delta={delta:e}: naive={naiveError:e4}, poly={polyError:e4}");

        Assert.Equal(0d, naiveError);
        Assert.True(polyError > naiveError,
            $"delta={delta:e}: expected polynomial inverse to lose precision");
    }

    // Counterexample 5: Irrational scale factors.
    //
    // For p(x) = π*x, Simplify can't reduce (π, 1) via GCD since π is
    // irrational. Composing Poly(π)^13 * Poly(1/π)^13 produces
    // (π^13, π^13, 0). The Simplify method attempts to scale these to
    // integers, but π^13 ≈ 2.7e6 is not an exact integer after scaling.
    // The GCD path thus fails to reduce, and the final division π^13/π^13
    // may not cancel to exactly 1. Naive Math.Pow benefits from the same
    // FP representation and sometimes lands on exactly 1.
    [Theory]
    [InlineData(3)]
    [InlineData(13)]
    public void IrrationalScaleFactorRoundTrip(Int32 repetitions)
    {
        const Double value = E;

        // Naive: π^n * (1/π)^n * value
        Double naiveResult = Algorithms.Pow(PI, repetitions)
                           * Algorithms.Pow(1d / PI, repetitions) * value;
        Double naiveError = Abs(naiveResult - value);

        // Polynomial: Poly(π)^n * Poly(1/π)^n
        var scaleUp = Poly(nominator: PI);
        var scaleDown = Poly(denominator: PI);
        var roundTrip = scaleUp.Pow(repetitions) * scaleDown.Pow(repetitions);
        Double polyResult = roundTrip * value;
        Double polyError = Abs(polyResult - value);

        output.WriteLine(
            $"reps={repetitions}: naive={naiveError:e4}, poly={polyError:e4}");

        Assert.True(polyError >= naiveError,
            $"reps={repetitions}: expected polynomial to be no better than naive");
    }

    private static Double SafeRatio(Double a, Double b) => b == 0d ? Double.PositiveInfinity : a / b;
}
