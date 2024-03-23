using System.Numerics;
using Quantities.Core.Numerics;
using Quantities.Measures;

namespace Quantities.Core;

internal sealed class Result
    : ICastOperators<Result, Measure>
    , IMultiplyOperators<Result, Double, Double>
{
    public static Result Identity { get; } = new(Polynomial.One, Measure.Of<Identity>());
    private readonly Measure measure;
    private readonly Polynomial conversion;
    public Result(in Polynomial conversion, Measure result) => (this.conversion, this.measure) = (conversion.Simplify(), result);
    public static Double operator *(Result left, Double right) => left.conversion * right;
    public static explicit operator Polynomial(Result self) => self.conversion;
    public static implicit operator Measure(Result self) => self.measure;
}
