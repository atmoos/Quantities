using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Creation;

internal delegate Measure MeasureSelector(Factory factory);

internal interface ICreator
{
    public Quantity Create(in Double value);
}

public readonly struct Scalar<TUnit> : ICreator
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
    internal Quantity Create(in Double value, MeasureSelector selectMeasure) => new(in value, selectMeasure(this.factory));
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create());
    internal Quantity Transform(in Quantity other, MeasureSelector selectMeasure) => other.Project(selectMeasure(this.factory));
    Quantity ICreator.Create(in Double value) => Create(in value);
}

public readonly struct Product<TLeft, TRight> : ICreator
    where TLeft : IUnit, IDimension
    where TRight : IUnit, IDimension
{
    private readonly Factory factory;
    internal Product(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Create());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create());
    Quantity ICreator.Create(in Double value) => Create(in value);
}

public readonly struct Quotient<TN, TD> : ICreator
    where TN : IUnit, IDimension
    where TD : IUnit, IDimension
{
    private readonly Factory factory;
    internal Quotient(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Create());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create());
    Quantity ICreator.Create(in Double value) => Create(in value);
}

public readonly struct Square<TUnit> : ICreator
    where TUnit : IUnit, IDimension
{
    private readonly Factory factory;
    internal Square(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Square());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Square());
    Quantity ICreator.Create(in Double value) => Create(in value);
}

public readonly struct Cubic<TUnit> : ICreator
    where TUnit : IUnit, IDimension
{
    private readonly Factory factory;
    internal Cubic(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Cubic());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Cubic());
    Quantity ICreator.Create(in Double value) => Create(in value);
}
