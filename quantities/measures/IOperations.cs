namespace Quantities.Measures;

internal interface IOperations
{
    Quant Multiply<TMeasure>(in Double value) where TMeasure : IMeasure;
}

internal sealed class FromScalar<TScalarMeasure> : IOperations
    where TScalarMeasure : IMeasure
{
    public Quant Multiply<TMeasure>(in Double value) where TMeasure : IMeasure
    {
        return Build<Product<TScalarMeasure, TMeasure>>.With(in value);
    }
}
internal sealed class FromProduct<TLeft, TRight> : IOperations
    where TLeft : IMeasure
    where TRight : IMeasure
{
    public Quant Multiply<TMeasure>(in Double value) where TMeasure : IMeasure
    {
        // ToDo: TRight * TMeasure = TRight^2 !!!
        return Build<Product<TLeft, Power<Square, TRight>>>.With(in value);
    }
}
internal sealed class FromQuotient<TNominator, TDenominator> : IOperations
    where TNominator : IMeasure
    where TDenominator : IMeasure
{
    public Quant Multiply<TMeasure>(in Double value) where TMeasure : IMeasure
    {
        var poly = Conversion<TMeasure, TDenominator>.Polynomial;
        return Build<TNominator>.With(poly.Evaluate(in value));
    }
}

internal sealed class FromPower<TDim, TScalarMeasure> : IOperations
    where TDim : IDimension
    where TScalarMeasure : IMeasure
{
    public Quant Multiply<TMeasure>(in Double value) where TMeasure : IMeasure
    {
        return Build<Product<Power<TDim, TScalarMeasure>, TMeasure>>.With(in value);
    }
}