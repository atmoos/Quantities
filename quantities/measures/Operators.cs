using Quantities.Numerics;
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
        if (this.comparison.Is<TMeasure>()) {
            var (scaled, _) = TMeasure.Lower(Empty.Injection, in value);
            return TScalarMeasure.Normalize(scaling, Power<Square>.Injection, scaled);
        }
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
            return Ops<TLeft>.Reduce<TRight, TMeasure>(scaling, in operands);
        }
        if (left.Is<TMeasure>()) {
            return Ops<TRight>.Reduce<TLeft, TMeasure>(scaling, in operands);
        }
        return Product<TLeft, Quotient<TRight, TMeasure>>.Normalize(scaling, Linear.Injection, operands.Left / operands.Right);
    }
    public Quant Multiply<TMeasure>(IPrefixScale scaling, in Double value) where TMeasure : IMeasure
    {
        if (right.Is<TMeasure>()) {
            return Ops<TLeft>.Square<TRight, TMeasure>(scaling, in value);
        }
        if (left.Is<TMeasure>()) {
            return Ops<TRight>.Square<TLeft, TMeasure>(scaling, in value);
        }
        return Product<TLeft, Product<TRight, TMeasure>>.Normalize(scaling, Linear.Injection, in value);
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

file static class Empty
{
    public static IInject<Quant> Injection { get; } = new Injector();
    private sealed class Injector : IInject<Quant>
    {
        public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure => default;
    }
}
file static class Linear
{
    public static IInject<Quant> Injection { get; } = new Injector();
    private sealed class Injector : IInject<Quant>
    {
        public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<TMeasure>.With(in value);
    }
}

file static class Power<TDim>
    where TDim : IDimension
{
    public static IInject<Quant> Injection { get; } = new Injector();
    public static IInject<IInject<Quant>> Product { get; } = new ProductOfSquare();
    private sealed class Injector : IInject<Quant>
    {
        public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Power<TDim, TMeasure>>.With(in value);
    }

    private sealed class ProductOfSquare : IInject<IInject<Quant>>
    {
        public IInject<Quant> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => AllocationFree<ToSquare<TMeasure>>.Item;
    }
    private sealed class ToSquare<TLinear> : IInject<Quant>
        where TLinear : IMeasure
    {
        public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure
        {
            return Build<Product<TMeasure, Power<TDim, TLinear>>>.With(in value);
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
            public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Product<TMeasure, TRight>>.With(in value);
        }
    }
    private sealed class QuotientInjector : IInject<IInject<Quant>>
    {
        public IInject<Quant> Inject<TMeasure>(in Double value) where TMeasure : IMeasure => AllocationFree<Quotient<TMeasure>>.Item;

        private sealed class Quotient<TRight> : IInject<Quant>
            where TRight : IMeasure
        {
            public Quant Inject<TMeasure>(in Double value) where TMeasure : IMeasure => Build<Quotient<TMeasure, TRight>>.With(in value);
        }
    }
}

file static class Ops<TResult>
    where TResult : IMeasure
{
    public static Quant Square<TLeft, TRight>(IPrefixScale scaling, in Double value)
        where TLeft : IMeasure
        where TRight : IMeasure
    {
        var siPoly = Polynomial.Of<TRight>();
        var (left, injection) = TLeft.Lower(Power<Square>.Product, siPoly.Evaluate(in value));
        return TResult.Normalize(scaling, injection, left);
    }
    public static Quant Reduce<TNominator, TDenominator>(IPrefixScale scaling, in Operands operands)
        where TNominator : IMeasure
        where TDenominator : IMeasure
    {
        var poly = Conversion<TDenominator, TNominator>.Polynomial;
        return TResult.Normalize(scaling, Linear.Injection, operands.Left / poly.Evaluate(in operands.Right));
    }
}
