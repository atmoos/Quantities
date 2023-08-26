using Quantities.Prefixes;

namespace Quantities.Measures;

internal abstract class Comparison
{
    protected Comparison() { }
    public abstract Boolean Is<TMeasure>() where TMeasure : IMeasure;
}

file sealed class Comparison<TDimension> : Comparison
    where TDimension : Dimensions.IDimension
{
    public override Boolean Is<TMeasure>() => TMeasure.Is<TDimension>();
}

internal sealed class FromScalar<TScalarMeasure> : IOperations
    where TScalarMeasure : IMeasure
{
    private readonly Comparison comparison;
    private FromScalar(Comparison comparison) => this.comparison = comparison;
    public Boolean Is<TMeasure>() where TMeasure : IMeasure => this.comparison.Is<TMeasure>();
    public Quant Divide<TMeasure>(IPrefixScale scaling, in Operands operands) where TMeasure : IMeasure
    {
        var (loweredDenominator, right) = TMeasure.Lower(Operator.Quotient, in operands.Right);
        return TScalarMeasure.Normalize(scaling, right, operands.Left / loweredDenominator);
    }
    public Quant Multiply<TMeasure>(IPrefixScale scaling, in Double value) where TMeasure : IMeasure
    {
        var (lowered, right) = TMeasure.Lower(Operator.Product, in value);
        return TScalarMeasure.Normalize(scaling, right, in lowered);
    }
    public static IOperations Create<TDimension>() where TDimension : Dimensions.IDimension
    {
        return new FromScalar<TScalarMeasure>(AllocationFree<Comparison<TDimension>>.Item);
    }
}
internal sealed class FromProduct<TLeft, TRight> : IOperations
    where TLeft : IMeasure
    where TRight : IMeasure
{
    private static readonly IOperations left = TLeft.Operations;
    private static readonly IOperations right = TRight.Operations;
    public Boolean Is<TMeasure>() where TMeasure : IMeasure => false; // ToDo!
    public Quant Divide<TMeasure>(IPrefixScale scaling, in Operands operands) where TMeasure : IMeasure
    {
        if (right.Is<TMeasure>()) {
            return Divide<TLeft, TRight, TMeasure>(scaling, in operands);
        }
        if (left.Is<TMeasure>()) {
            return Divide<TRight, TLeft, TMeasure>(scaling, in operands);
        }
        return Product<TLeft, Quotient<TRight, TMeasure>>.Normalize(scaling, Linear.Injection, operands.Left / operands.Right);
    }
    public Quant Multiply<TMeasure>(IPrefixScale scaling, in Double value) where TMeasure : IMeasure
    {
        // ToDo: TRight * TMeasure = TRight^2 !!!
        return Build<Product<TLeft, Power<Square, TRight>>>.With(in value);
    }

    private static Quant Divide<TLinear, TNominator, TDenominator>(IPrefixScale scaling, in Operands operands)
        where TLinear : IMeasure
        where TNominator : IMeasure
        where TDenominator : IMeasure
    {
        var poly = Conversion<TDenominator, TNominator>.Polynomial;
        return TLinear.Normalize(scaling, Linear.Injection, operands.Left / poly.Evaluate(in operands.Right));
    }
}
internal sealed class FromQuotient<TNominator, TDenominator> : IOperations
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    public Boolean Is<TMeasure>() where TMeasure : IMeasure => false; // ToDo!
    public Quant Divide<TMeasure>(IPrefixScale scaling, in Operands operands) where TMeasure : IMeasure
    {
        var poly = Conversion<TMeasure, TNominator>.Polynomial;
        return TDenominator.Normalize(scaling, Linear.Injection, poly.Evaluate(in operands.Left) / operands.Right);
    }
    public Quant Multiply<TMeasure>(IPrefixScale scaling, in Double value) where TMeasure : IMeasure
    {
        var poly = Conversion<TMeasure, TDenominator>.Polynomial;
        return TNominator.Normalize(scaling, Linear.Injection, poly.Evaluate(in value));
    }
}

internal sealed class FromPower<TDim, TScalarMeasure> : IOperations
    where TDim : IDimension
    where TScalarMeasure : IMeasure
{
    public Boolean Is<TMeasure>() where TMeasure : IMeasure => false; // ToDo!
    public Quant Divide<TMeasure>(IPrefixScale scaling, in Operands operands) where TMeasure : IMeasure
    {
        throw new NotImplementedException();
    }
    public Quant Multiply<TMeasure>(IPrefixScale scaling, in Double value) where TMeasure : IMeasure
    {
        return Build<Product<Power<TDim, TScalarMeasure>, TMeasure>>.With(in value);
    }
}

file static class Linear
{
    public static IInject<Quant> Injection { get; } = new Implementation();

    private sealed class Implementation : IInject<Quant>
    {
        public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure
        {
            return Build<TMeasure>.With(in value);
        }
    }
}

file static class Operator
{
    public static IInject<IInject<Quant>> Product { get; } = new ProductInjector();
    public static IInject<IInject<Quant>> Quotient { get; } = new QuotientInjector();
    private sealed class ProductInjector : IInject<IInject<Quant>>
    {
        public IInject<Quant> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => AllocationFree<Product<TMeasure>>.Item;

        private sealed class Product<TRight> : IInject<Quant>
            where TRight : IMeasure
        {
            public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure
            {
                return Build<Product<TMeasure, TRight>>.With(in value);
            }
        }
    }
    private sealed class QuotientInjector : IInject<IInject<Quant>>
    {
        public IInject<Quant> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => AllocationFree<Quotient<TMeasure>>.Item;

        private sealed class Quotient<TRight> : IInject<Quant>
            where TRight : IMeasure
        {
            public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure
            {
                return Build<Quotient<TMeasure, TRight>>.With(in value);
            }
        }
    }
}

