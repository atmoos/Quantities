using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities.Creation;

public readonly struct Scalar<TUnit>
    where TUnit : IUnit, IDimension
{
    private readonly Factory factory;
    internal Factory Factory => this.factory;
    internal Scalar(Factory factory) => this.factory = factory;
    public Product<TUnit, TRight> Dot<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension => new(rightTerm.factory.Inject(this.factory.Product));
    public Quotient<TUnit, TDenominator> Per<TDenominator>(in Scalar<TDenominator> denominator)
        where TDenominator : IUnit, IDimension => new(denominator.factory.Inject(this.factory.Quotient));
    internal Quantity Create(in Double value) => new(in value, this.factory.Create());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create());
}

public readonly struct Alias<TUnit>
    where TUnit : IAlias, IUnit, IDimension
{
    private readonly Measure measure;
    internal Alias(Measure measure) => this.measure = measure;
    internal Quantity Create(in Double value) => new(in value, in this.measure);
    internal Quantity Transform(in Quantity other) => other.Project(in this.measure);
}

public readonly struct Product<TLeft, TRight>
    where TLeft : IUnit, IDimension
    where TRight : IUnit, IDimension
{
    private readonly Factory factory;
    internal Product(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Create());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create());
}

public readonly struct Quotient<TN, TD>
    where TN : IUnit, IDimension
    where TD : IUnit, IDimension
{
    private readonly Factory factory;
    internal Quotient(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Create());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create());
}

public readonly struct Square<TDim>
    where TDim : IUnit, IDimension
{
    private readonly Factory factory;
    internal Square(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Square());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Square());
}

public readonly struct Cubic<TDim>
    where TDim : IUnit, IDimension
{
    private readonly Factory factory;
    internal Cubic(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Cubic());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Cubic());
}
