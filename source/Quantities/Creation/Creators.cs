using Quantities.Dimensions;
using Quantities.Units;

namespace Quantities.Creation;

public readonly struct Scalar<TUnit>
    where TUnit : IUnit, IDimension
{
    private readonly Factory factory;
    internal Factory Factory => this.factory;
    internal Scalar(Factory factory) => this.factory = factory;
    public Product<TUnit, TRight> Times<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension => new(rightTerm.factory.Inject(this.factory.Product));
    public Quotient<TUnit, TDenominator> Per<TDenominator>(in Scalar<TDenominator> denominator)
        where TDenominator : IUnit, IDimension => new(denominator.factory.Inject(this.factory.Quotient));
    internal Quantity Create(in Double value) => new(in value, this.factory.Create());
    internal Quantity Create<TAlias, TLinear>(in Double value)
        where TAlias : TUnit, IAlias<TLinear>
        where TLinear : ILinear, IDimension => new(in value, this.factory.AliasTo<TAlias, TLinear>());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create());
    internal Quantity Transform<TAlias, TLinear>(in Quantity other)
        where TAlias : TUnit, IAlias<TLinear>
        where TLinear : ILinear, IDimension => other.Project(this.factory.AliasTo<TAlias, TLinear>());
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

public readonly struct Square<TUnit>
    where TUnit : IUnit, IDimension
{
    private readonly Factory factory;
    internal Square(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Square());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Square());
}

public readonly struct Cubic<TUnit>
    where TUnit : IUnit, IDimension
{
    private readonly Factory factory;
    internal Cubic(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Cubic());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Cubic());
}
