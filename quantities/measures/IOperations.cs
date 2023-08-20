using Quantities.Prefixes;

namespace Quantities.Measures;

internal interface IOperations
{
    Quant Divide<TMeasure>(IPrefixScale scaling, in Operands operands) where TMeasure : IMeasure;
    Quant Multiply<TMeasure>(IPrefixScale scaling, in Double value) where TMeasure : IMeasure;
}

internal sealed class FromScalar<TScalarMeasure> : IOperations
    where TScalarMeasure : IMeasure
{
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
}
internal sealed class FromProduct<TLeft, TRight> : IOperations
    where TLeft : IMeasure
    where TRight : IMeasure
{
    public Quant Divide<TMeasure>(IPrefixScale scaling, in Operands operands) where TMeasure : IMeasure
    {
        var poly = Conversion<TMeasure, TRight>.Polynomial;
        return TLeft.Normalize(scaling, Linear.Injection, operands.Left / poly.Evaluate(in operands.Right));
    }
    public Quant Multiply<TMeasure>(IPrefixScale scaling, in Double value) where TMeasure : IMeasure
    {
        // ToDo: TRight * TMeasure = TRight^2 !!!
        return Build<Product<TLeft, Power<Square, TRight>>>.With(in value);
    }
}
internal sealed class FromQuotient<TNominator, TDenominator> : IOperations
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    public Quant Divide<TMeasure>(IPrefixScale scaling, in Operands operands) where TMeasure : IMeasure
    {
        throw new NotImplementedException();
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

