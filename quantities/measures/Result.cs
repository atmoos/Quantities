using System.Numerics;
using Quantities.Numerics;

namespace Quantities.Measures;

internal sealed class Result
    : ICastOperators<Result, Measure>
    , IMultiplyOperators<Result, Double, Double>
{
    private readonly Polynomial poly;
    private readonly Measure result;
    public Result(in Polynomial poly, Measure result) => (this.poly, this.result) = (poly, result);
    public static Double operator *(Result left, Double right) => left.poly * right;
    public static implicit operator Measure(Result self) => self.result;
}
