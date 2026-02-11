using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Creation;

internal delegate ref readonly Measure MeasureSelector(Factory factory);

// Note 5. Sept. 2025: Changing these to ref structs doesn't improve performance.

public readonly struct Scalar<TUnit>
    where TUnit : IUnit, IDimension
{
    private readonly Factory factory;
    internal Factory Factory => this.factory;

    internal Scalar(in Factory factory) => this.factory = factory;

    public Product<TUnit, TRight> Times<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension => new(in this.factory.Multiply(rightTerm.Factory));

    public Product<TUnit, TRight> Times<TRight, TExponent>(in Power<TRight, TExponent> rightTerm)
        where TRight : IUnit, IDimension
        where TExponent : INumber, IPositive => new(in this.factory.Multiply(rightTerm.Factory));

    public Quotient<TUnit, TDenominator> Per<TDenominator>(in Scalar<TDenominator> denominator)
        where TDenominator : IUnit, IDimension => new(in this.factory.Divide(denominator.Factory));

    public Quotient<TUnit, Power<TDenominator, TExponent>> Per<TDenominator, TExponent>(in Power<TDenominator, TExponent> denominator)
        where TDenominator : IUnit, IDimension
        where TExponent : INumber, IPositive => new(in this.factory.Divide(denominator.Factory));

    internal Quantity Create(in Double value) => new(in value, in this.factory.Create());

    internal Quantity Create(in Double value, MeasureSelector selectMeasure) => new(in value, in selectMeasure(this.factory));

    internal Quantity Transform(in Quantity other) => other.Project(in this.factory.Create());

    internal Quantity Transform(in Quantity other, MeasureSelector selectMeasure) => other.Project(in selectMeasure(this.factory));
}

public readonly struct Product<TLeft, TRight>
{
    private readonly Factory factory;

    internal Product(in Factory factory) => this.factory = factory;

    internal Quantity Create(in Double value) => new(in value, in this.factory.Create());

    internal Quantity Transform(in Quantity other) => other.Project(in this.factory.Create());
}

public readonly struct Quotient<TN, TD>
{
    private readonly Factory factory;

    internal Quotient(in Factory factory) => this.factory = factory;

    internal Quantity Create(in Double value) => new(in value, in this.factory.Create());

    internal Quantity Transform(in Quantity other) => other.Project(in this.factory.Create());
}

public readonly struct Power<TUnit, TExponent>
    where TUnit : IUnit, IDimension
    where TExponent : INumber
{
    private readonly Factory factory;
    internal Factory Factory => this.factory;

    internal Power(in Factory factory) => this.factory = factory.Power<TExponent>();

    internal Quantity Create(in Double value) => new(in value, in this.factory.Create());

    internal Quantity Transform(in Quantity other) => other.Project(in this.factory.Create());
}
