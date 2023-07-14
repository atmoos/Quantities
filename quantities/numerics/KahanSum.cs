using System.Runtime.CompilerServices;

namespace Quantities.Numerics;

/// <summary>
/// Implementation <see href="https://en.wikipedia.org/wiki/Kahan_summation_algorithm"/>
/// </summary>
public sealed class KahanSummation
{
    private Double sum;
    private Double compensation;
    public KahanSummation(Double initialValue = 0d)
    {
        this.compensation = 0d;
        this.sum = initialValue;
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    public KahanSummation Add(Double summand)
    {
        var y = summand - this.compensation;
        var t = this.sum + y;
        this.compensation = (t - this.sum) - y;
        this.sum = t;
        return this;
    }
    public KahanSummation Subtract(Double summand) => Add(-summand);


    public static implicit operator Double(KahanSummation summation) => summation.sum;
}
