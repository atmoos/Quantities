
using Quantities.Measures;
using Quantities.Units.Si.Metric;

namespace Quantities.Test.Measures;

public interface IMultiplyProperties
{
}
public interface IDivisionProperties
{

}

public class OperatorsTest
{
    private static readonly IPrefixScale scaling = Metric.TriadicScaling;
    public class ScalarOperator : IMultiplyProperties, IDivisionProperties
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

        [Fact]
        public void MultiplyWithPrefixedDimensionReturnsSquaredDimension()
        {
            var expected = SquareOf<Si<Milli, Metre>>(4);
            var scalar = Scalar<Si<Micro, Metre>, Metre>();
            // 4 * km * μm = 4 mm  
            var actual = scalar.Multiply<Si<Kilo, Metre>>(scaling, 4);

            actual.IsSameAs(expected);
        }

        [Fact]
        public void DivideWithSameDimensionReturnsQuotient()
        {
            var expected = QuotientOf<Si<Metre>, Si<Metre>>(2);
            var actual = scalar.Divide<Si<Metre>>(scaling, new Operands(8, 4));

            actual.IsSameAs(expected);
        }

        [Fact]
        public void DivideWithPrefixedDimensionReturnsNormalizedQuotient()
        {
            var expected = QuotientOf<Si<Kilo, Metre>, Si<Second>>(20);
            var scalar = Scalar<Si<Deca, Metre>, Metre>();
            // (8 * dm) / (4 ms) = 20 (km)/s
            var actual = scalar.Divide<Si<Milli, Second>>(scaling, new Operands(8, 4));

            actual.IsSameAs(expected);
        }
    }
    public class ProductOperator
    {
        private static readonly IOperations product = Product<Si<Metre>, Si<Second>>();

        [Fact]
        public void MultiplicationSquaresRightTermWhenRightArgumentIsSameDimension()
        {
            var expected = ProductOf<Si<Kilo, Metre>, Power<Square, Si<Second>>>(4 * 3.6);
            var actual = product.Multiply<Metric<Hour>>(scaling, 4);

            actual.IsSameAs(expected);
        }

        [Fact]
        public void DivisionSelectsLeftTermWhenDivisorIsSameDimensionAsRightTerm()
        {
            var expected = ScalarOf<Si<Kilo, Metre>>(4);
            var actual = product.Divide<Si<Milli, Second>>(scaling, new Operands(8, 2));

            actual.IsSameAs(expected);
        }

        [Fact]
        public void DivisionSelectsRightTermWhenDivisorIsSameDimensionAsLeftTerm()
        {
            var expected = ScalarOf<Si<Micro, Second>>(4);
            var actual = product.Divide<Si<Kilo, Metre>>(scaling, new Operands(8, 2000));

            actual.IsSameAs(expected);
        }

        [Fact]
        public void DivisionByNewDimensionIsQuotient()
        {
            var product = Product<Si<Metre>, Si<Milli, Second>>();
            var expected = ProductOf<Si<Kilo, Metre>, Quotient<Si<Second>, Si<Kelvin>>>(4);
            var actual = product.Divide<Si<Micro, Kelvin>>(scaling, new Operands(8, 2));

            actual.IsSameAs(expected);
        }
    }
    public class QuotientOperator : IMultiplyProperties, IDivisionProperties
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
