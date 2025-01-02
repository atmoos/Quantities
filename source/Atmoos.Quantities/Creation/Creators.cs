using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Measures;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Creation;

internal delegate Measure MeasureSelector(Factory factory);

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
    internal Quantity Create(in Double value, MeasureSelector selectMeasure) => new(in value, selectMeasure(this.factory));
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create());
    internal Quantity Transform(in Quantity other, MeasureSelector selectMeasure) => other.Project(selectMeasure(this.factory));
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

// ToDo: Consider renaming this to PositivePowerOf or the like...
public readonly struct Power<TUnit, TExponent>
    where TUnit : IUnit, IDimension
    where TExponent : INumber
{
    private readonly Factory factory;
    internal Power(Factory factory) => this.factory = factory;
    internal Quantity Create(in Double value) => new(in value, this.factory.Create<Numerator<TExponent>>());
    internal Quantity Transform(in Quantity other) => other.Project(this.factory.Create<Numerator<TExponent>>());
}
