using System.Runtime.CompilerServices;

namespace Quantities.Benchmark.Numerics;

public static class Trivial
{

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Double Poly(in (Double a, Double b, Double c) poly, in Double value) => poly.a * value / poly.b + poly.c;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static (Double a, Double b, Double c) PolyExp(in (Double a, Double b, Double c) poly, Int32 exp)
    {
        exp = Math.Abs(exp);
        var result = (a: 1d, b: 1d, c: 0d);
        for (Int32 e = 1; e <= exp; ++e) {
            result.c += Poly(in result, result.c);
            result.a *= poly.a;
            result.b *= poly.b;
        }
        return result;
    }
}
