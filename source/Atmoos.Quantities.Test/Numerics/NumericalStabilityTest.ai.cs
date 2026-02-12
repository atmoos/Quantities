using Atmoos.Quantities.Core.Numerics;
using static System.Math;
using static Atmoos.Quantities.Test.Convenience;
using static Atmoos.Quantities.TestTools.Convenience;

namespace Atmoos.Quantities.Test.Numerics;

// These tests demonstrate that the library's Polynomial layer is more
// numerically stable than "just using doubles". Each test shows a scenario
// where naive double arithmetic accumulates error, while the Polynomial
// representation (rational fraction + FMA + GCD simplification) retains
// significantly more precision.
public class NumericalStabilityTest
{
    // Scenario 1: Chained scaling
    // Multiplying and dividing by a large factor (e.g. 1e18) then comparing
    // to the identity. Naive doubles lose precision because intermediate
    // values are rounded after each operation.
    [Fact]
    public void ChainedScalingPreservesMorePrecisionThanNaiveDoubles()
    {
        const Double value = PI;
        const Double scale = 1e18;

        // Naive: apply scale then undo it manually.
        Double naiveResult = value * scale / scale;
        Double naiveError = Abs(naiveResult - value);

        // Polynomial: compose p(x)=scale*x with its inverse p(x)=x/scale,
        // then evaluate. The rational representation cancels exactly via GCD.
        var scaleUp = Poly(nominator: scale);     // p(x) = scale * x
        var scaleDown = Poly(denominator: scale); // q(x) = x / scale
        var roundTrip = scaleUp * scaleDown;      // compose symbolically
        Double polyResult = roundTrip * value;
        Double polyError = Abs(polyResult - value);

        // The polynomial achieves strictly less (or equal) error.
        Assert.True(polyError <= naiveError,
            $"Polynomial error ({polyError:e}) should be <= naive error ({naiveError:e})");
        // And (for this case) the polynomial is actually exact.
        Assert.Equal(value, polyResult);
    }

    // Scenario 2: Deep composition chain
    // Composing many affine transformations (with offsets) sequentially
    // and then composing their inverses. With naive doubles, each
    // composition step introduces rounding; the Polynomial layer defers
    // evaluation and simplifies symbolically.
    [Fact]
    public void DeepCompositionChainIsMoreStableThanNaiveDoubles()
    {
        const Double value = E;
        const Int32 depth = 12;
        // An affine transform: f(x) = 13x/7 + 3
        const Double n = 13d, d = 7d, offset = 3d;

        // --- Naive double chain ---
        Double naiveForward = value;
        for (Int32 i = 0; i < depth; i++) {
            naiveForward = n / d * naiveForward + offset;
        }
        Double naiveBackward = naiveForward;
        for (Int32 i = 0; i < depth; i++) {
            naiveBackward = d / n * (naiveBackward - offset);
        }
        Double naiveError = Abs(naiveBackward - value);

        // --- Polynomial chain ---
        var poly = Poly(n, d, offset);
        var forward = poly;
        for (Int32 i = 1; i < depth; i++) {
            forward = poly * forward; // symbolic composition
        }
        var inverse = forward; // apply forward...
        for (Int32 i = 0; i < depth; i++) {
            inverse = Polynomial.One / poly * inverse; // ...then undo
        }
        // The inverse chain ideally collapses back to identity.
        Double polyResult = inverse * value;
        Double polyError = Abs(polyResult - value);

        Assert.True(polyError <= naiveError,
            $"Polynomial error ({polyError:e}) should be <= naive error ({naiveError:e})");
    }

    // Scenario 3: Power round-trip (p^n * p^-n == identity)
    // Raising a polynomial to a high power and back should yield identity.
    // With naive doubles, exponentiation amplifies rounding error.
    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(11)]
    public void PowerRoundTripIsMoreStableThanNaiveDoubles(Int32 exponent)
    {
        const Double value = Tau / E;
        const Double n = 13d, d = 14d;

        // Naive: (n/d)^exp * (d/n)^exp * value
        Double naiveScale = Pow(n / d, exponent);
        Double naiveInverse = Pow(d / n, exponent);
        Double naiveResult = naiveScale * naiveInverse * value;
        Double naiveError = Abs(naiveResult - value);

        // Polynomial: symbolic Pow then compose.
        var poly = Poly(n, d);
        var power = poly.Pow(exponent);
        var inversePower = poly.Pow(-exponent);
        var roundTrip = power * inversePower;
        Double polyResult = roundTrip * value;
        Double polyError = Abs(polyResult - value);

        Assert.True(polyError <= naiveError,
            $"exp={exponent}: Polynomial error ({polyError:e}) should be <= naive error ({naiveError:e})");
    }

    // Scenario 4: Chaining through many scale factors
    // Multiplying and dividing by 1000 twenty times each using naive
    // doubles accumulates rounding at every step (~40 operations).
    // The Polynomial layer composes all steps symbolically and simplifies
    // the result to an exact identity before evaluating — zero drift.
    [Fact]
    public void ChainingThroughManyScaleFactorsAccumulatesLessDriftThanNaiveDoubles()
    {
        const Double value = E;
        const Int32 steps = 20;
        const Double factor = 1000d;

        // Naive: multiply by 1000 twenty times, then divide by 1000 twenty times.
        // Each step can introduce up to 0.5 ULP of rounding error.
        Double naive = value;
        for (Int32 i = 0; i < steps; i++) naive *= factor;
        for (Int32 i = 0; i < steps; i++) naive /= factor;
        Double naiveError = Abs(naive - value);

        // Polynomial: compose the scalings symbolically, then simplify and evaluate once.
        // The composition yields (1000^20, 1000^20, 0), which simplifies to (1, 1, 0).
        var up = Poly(nominator: factor);
        var down = Poly(denominator: factor);
        var composed = Polynomial.One;
        for (Int32 i = 0; i < steps; i++) composed = up * composed;
        for (Int32 i = 0; i < steps; i++) composed = down * composed;
        composed = composed.Simplify();
        Double polyResult = composed * value;
        Double polyError = Abs(polyResult - value);

        // The polynomial layer eliminates all drift; naive accumulates ~20 ULPs.
        Assert.True(polyError <= naiveError,
            $"Polynomial error ({polyError:e}) should be <= naive error ({naiveError:e})");
        Assert.Equal(0d, polyError); // exact identity!
    }

    // Scenario 5: GCD simplification keeps coefficients small
    // When composing polynomials, nominators and denominators grow
    // multiplicatively. Simplification via GCD keeps them small,
    // which preserves precision in subsequent operations.
    [Fact]
    public void GcdSimplificationKeepsCoefficientsMaintainable()
    {
        Double value = Sqrt(2d);
        // Poly(12, 18) stores (12, 18)—the Poly helper does not auto-simplify.
        // But Simplify() reduces it to (2, 3) via GCD(12, 18) = 6.
        var poly = Poly(12, 18);
        var simplified = poly.Simplify();
        var (n, d, _) = simplified;
        Assert.Equal(2d, n);
        Assert.Equal(3d, d);

        // Demonstrate that p^n * p^{-n} yields identity on the simplified form.
        // The rational representation avoids the rounding that naive doubles incur.
        const Int32 exp = 10;
        var power = simplified.Pow(exp);
        var inversePower = simplified.Pow(-exp);
        var roundTrip = power * inversePower;
        var (rn, rd, ro) = roundTrip.Simplify();

        // Polynomial round-trip is exact: n=1, d=1, offset=0.
        Assert.Equal(1d, rn);
        Assert.Equal(1d, rd);
        Assert.Equal(0d, ro);

        // Naive: ((2/3)^10) * ((3/2)^10) * value
        Double naiveScale = Pow(2d / 3d, exp);
        Double naiveInverse = Pow(3d / 2d, exp);
        Double naiveResult = naiveScale * naiveInverse * value;

        // The polynomial is exact; naive accumulates floating-point drift.
        Double polyResult = roundTrip * value;
        Assert.Equal(value, polyResult); // exact
        Assert.True(Abs(naiveResult - value) >= Abs(polyResult - value),
            $"Polynomial should be at least as precise as naive doubles");
    }

    // Scenario 6: FusedMultiplyAdd vs. separate multiply-then-add
    // FMA computes (a * b + c) with a single rounding step, while
    // separate operations round twice. This matters for unit conversions
    // that involve both scaling and offset (temperature conversions).
    [Fact]
    public void FmaIsMorePreciseThanSeparateMultiplyAndAdd()
    {
        // Large nominator/denominator with non-trivial offset, evaluated
        // at a value that causes the product and offset to nearly cancel.
        const Double n = 1_000_000_000_000_001d;
        const Double d = 1_000_000_000_000_000d;
        const Double offset = -1d;
        const Double x = 1d;
        // Exact: (n * x + d * offset) / d = (n - d) / d = 1e-15
        const Double expected = 1e-15;

        // Naive: n/d * x + offset = 1.000000000000001 - 1.0
        // This subtraction loses most significant digits.
        Double naiveResult = (n / d) * x + offset;
        Double naiveError = Abs(naiveResult - expected);

        // FMA-based Polynomial evaluation: FMA(n, x, d * offset) / d
        // = FMA(1e15+1, 1, -1e15) / 1e15 — the FMA keeps full precision
        // for the intermediate sum before the final division.
        var poly = Poly(n, d, offset);
        Double polyResult = poly * x;
        Double polyError = Abs(polyResult - expected);

        Assert.True(polyError <= naiveError,
            $"Polynomial error ({polyError:e}) should be <= naive error ({naiveError:e})");
    }

    // Scenario 7: Extreme prefix round-trips
    // Converting from Quecto (1e-30) to base and back. Even though the
    // mathematical result is exact, intermediate rounding in naive doubles
    // loses precision at these extreme scales.
    [Theory]
    [InlineData(1e-30)] // Quecto
    [InlineData(1e-24)] // Yocto
    [InlineData(1e18)]  // Exa
    [InlineData(1e30)]  // Quetta
    public void ExtremePrefixRoundTripsAreStable(Double prefixFactor)
    {
        const Double value = PI;

        // Naive: multiply by factor, then divide by factor.
        Double naiveResult = (value * prefixFactor) / prefixFactor;
        Double naiveError = Abs(naiveResult - value);

        // Polynomial: compose scaling with its inverse symbolically.
        var forward = Poly(nominator: prefixFactor);
        var backward = Poly(denominator: prefixFactor);
        var roundTrip = forward * backward;
        Double polyResult = roundTrip * value;
        Double polyError = Abs(polyResult - value);

        Assert.True(polyError <= naiveError,
            $"factor={prefixFactor:e}: Polynomial error ({polyError:e}) should be <= naive error ({naiveError:e})");
    }
}
