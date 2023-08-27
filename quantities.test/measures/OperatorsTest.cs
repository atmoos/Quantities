
using Quantities.Measures;

namespace Quantities.Test.Measures;

public interface IMultiplyTests
{
    void MultiplyNormalisesResult();
}
public interface IDivisionTests
{

}

public static class OperatorsTest
{
    private static readonly IPrefixScale scaling = Metric.TriadicScaling;
    public class ScalarOperator : IMultiplyTests, IDivisionTests
    {
        private static readonly IOperations scalar = Scalar<Si<Metre>, Metre>();

        [Fact]
        public void MultiplyNormalisesResult()
        {
            var expected = ProductOf<Si<Kilo, Metre>, Si<Second>>(40);
            var actual = scalar.Multiply<Si<Milli, Second>>(scaling, 4e7);

            actual.IsSameAs(expected);
        }

        [Fact]
        public void MultiplyWithSameDimensionReturnsSquaredDimension()
        {
            var expected = SquareOf<Si<Metre>>(4);
            var actual = scalar.Multiply<Si<Metre>>(scaling, 4);

            actual.IsSameAs(expected);
        }
    }

    public class QuotientOperator : IMultiplyTests, IDivisionTests
    {
        private static readonly IOperations quotient = Quotient<Si<Metre>, Si<Second>>();

        [Fact]
        public void MultiplyNormalisesResult()
        {
            var expected = ScalarOf<Si<Kilo, Metre>>(40);
            var actual = quotient.Multiply<Si<Milli, Second>>(scaling, 4e7);

            actual.IsSameAs(expected);
        }
    }

    private static IOperations Scalar<TMeasure, TDimension>()
        where TMeasure : IMeasure where TDimension : Dimensions.IDimension => FromScalar<TMeasure>.Create<TDimension>();
    private static IOperations Product<TLeft, TRight>()
        where TLeft : IMeasure where TRight : IMeasure => new FromProduct<TLeft, TRight>();
    private static IOperations Quotient<TNominator, TDenominator>()
        where TNominator : IMeasure where TDenominator : IMeasure => new FromQuotient<TNominator, TDenominator>();
    private static Quant ScalarOf<TMeasure>(Double value)
        where TMeasure : IMeasure => Build<TMeasure>.With(in value);
    private static Quant ProductOf<TLeft, TRight>(Double value)
        where TLeft : IMeasure where TRight : IMeasure => Build<Product<TLeft, TRight>>.With(in value);
    private static Quant QuotientOf<TNominator, TDenominator>(Double value)
        where TNominator : IMeasure where TDenominator : IMeasure => Build<Quotient<TNominator, TDenominator>>.With(in value);
    private static Quant SquareOf<TMeasure>(Double value)
        where TMeasure : IMeasure => Build<Power<Square, TMeasure>>.With(in value);
}
