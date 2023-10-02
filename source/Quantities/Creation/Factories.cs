using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities.Creation;

public readonly ref struct Scalar<TDim>
    where TDim : IUnit, IDimension
{
    private readonly Measure measure;
    internal Scalar(Measure measure) => this.measure = measure;

    // ToDo: Use injection here
    public Product<TDim, TRight> Dot<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension
    {
        var left = this.measure.Inject(Injectors.Product);
        return new(rightTerm.measure.Inject(left));
    }

    public Quotient<TDim, TDenominator> Per<TDenominator>(in Scalar<TDenominator> denominator)
        where TDenominator : IUnit, IDimension
    {
        var nominator = this.measure.Inject(Injectors.Quotient);
        return new(denominator.measure.Inject(nominator));
    }

    internal Quantity Create(in Double value) => new(in value, in this.measure);
    internal Quantity Transform(in Quantity other) => other.Project(in this.measure);
    internal Measure Cubic() => this.measure.Inject(Injectors.Cubic);
    internal Measure Square() => this.measure.Inject(Injectors.Square);
}

public readonly ref struct Alias<TUnit, TLinear>
    where TUnit : IAlias<TLinear>, IUnit, IDimension
    where TLinear : IDimension
{
    private readonly Measure measure;
    internal Alias(Measure measure) => this.measure = measure;
    internal Quantity Create(in Double value) => new(in value, in this.measure);
    internal Quantity Transform(in Quantity other) => other.Project(in this.measure);
}

public readonly ref struct Product<TLeft, TRight>
    where TLeft : IUnit, IDimension
    where TRight : IUnit, IDimension
{
    private readonly Measure measure;
    internal Product(Measure measure) => this.measure = measure;
    internal Quantity Create(in Double value) => new(in value, in this.measure);
    internal Quantity Transform(in Quantity other) => other.Project(in this.measure);
}

public readonly ref struct Quotient<TN, TD>
    where TN : IUnit, IDimension
    where TD : IUnit, IDimension
{
    private readonly Measure measure;
    internal Quotient(Measure measure) => this.measure = measure;
    internal Quantity Create(in Double value) => new(in value, in this.measure);
    internal Quantity Transform(in Quantity other) => other.Project(in this.measure);
}

public readonly ref struct Square<TDim>
    where TDim : IUnit, IDimension
{
    private readonly Measure measure;
    internal Square(Measure measure) => this.measure = measure;
    internal Quantity Create(in Double value) => new(in value, in this.measure);
    internal Quantity Transform(in Quantity other) => other.Project(in this.measure);
}

public readonly ref struct Cubic<TDim>
    where TDim : IUnit, IDimension
{
    private readonly Measure measure;
    internal Cubic(Measure measure) => this.measure = measure;
    internal Quantity Create(in Double value) => new(in value, in this.measure);
    internal Quantity Transform(in Quantity other) => other.Project(in this.measure);
}
