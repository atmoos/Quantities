using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities.Creation;

internal delegate ref readonly Measure MeasureSelector(Factory factory);

public interface ITimes<TTerm>
{
    public Product<TTerm, TRight> Times<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension;
    public Product<TTerm, Power<TRight, TExponent>> Times<TRight, TExponent>(in Power<TRight, TExponent> rightTerm)
        where TRight : IUnit, IDimension
        where TExponent : INumber, IPositive;
}

public interface IPer<TTerm>
{
    public Quotient<TTerm, TDenominator> Per<TDenominator>(in Scalar<TDenominator> denominator)
        where TDenominator : IUnit, IDimension;
    public Quotient<TTerm, Power<TDenominator, TExponent>> Per<TDenominator, TExponent>(in Power<TDenominator, TExponent> denominator)
        where TDenominator : IUnit, IDimension
        where TExponent : INumber, IPositive;
}

// Note 5. Sept. 2025: Changing these to ref structs doesn't improve performance.

public readonly struct Scalar<TUnit> : ITimes<TUnit>, IPer<TUnit>
    where TUnit : IUnit, IDimension
{
    private readonly Factory factory;
    internal Factory Factory => this.factory;

    internal Scalar(in Factory factory) => this.factory = factory;

    public Product<TUnit, TRight> Times<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension => new(in this.factory.Multiply(rightTerm.Factory));

    public Product<TUnit, Power<TRight, TExponent>> Times<TRight, TExponent>(in Power<TRight, TExponent> rightTerm)
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

public readonly struct Product<TLeftTerm, TRightTerm> : ITimes<Product<TLeftTerm, TRightTerm>>, IPer<Product<TLeftTerm, TRightTerm>>
{
    private readonly Factory factory;

    internal Product(in Factory factory) => this.factory = factory;

    public Product<Product<TLeftTerm, TRightTerm>, TRight> Times<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension => new(in this.factory.Multiply(rightTerm.Factory));

    public Product<Product<TLeftTerm, TRightTerm>, Power<TRight, TExponent>> Times<TRight, TExponent>(in Power<TRight, TExponent> rightTerm)
        where TRight : IUnit, IDimension
        where TExponent : INumber, IPositive => new(in this.factory.Multiply(rightTerm.Factory));

    public Quotient<Product<TLeftTerm, TRightTerm>, TDenominator> Per<TDenominator>(in Scalar<TDenominator> denominator) where TDenominator : IUnit, IDimension
        => new(in this.factory.Divide(denominator.Factory));

    public Quotient<Product<TLeftTerm, TRightTerm>, Power<TDenominator, TExponent>> Per<TDenominator, TExponent>(in Power<TDenominator, TExponent> denominator)
        where TDenominator : IUnit, IDimension
        where TExponent : INumber, IPositive => new(in this.factory.Divide(denominator.Factory));

    internal Quantity Create(in Double value) => new(in value, in this.factory.Create());

    internal Quantity Transform(in Quantity other) => other.Project(in this.factory.Create());
}

public readonly struct Quotient<TN, TD> : ITimes<Quotient<TN, TD>>, IPer<Quotient<TN, TD>>
{
    private readonly Factory factory;

    internal Quotient(in Factory factory) => this.factory = factory;

    public Product<Quotient<TN, TD>, TRight> Times<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension => new(in this.factory.Multiply(rightTerm.Factory));

    public Product<Quotient<TN, TD>, Power<TRight, TExponent>> Times<TRight, TExponent>(in Power<TRight, TExponent> rightTerm)
        where TRight : IUnit, IDimension
        where TExponent : INumber, IPositive => new(in this.factory.Multiply(rightTerm.Factory));

    public Quotient<Quotient<TN, TD>, TDenominator> Per<TDenominator>(in Scalar<TDenominator> denominator)
        where TDenominator : IUnit, IDimension => new(in this.factory.Divide(denominator.Factory));

    public Quotient<Quotient<TN, TD>, Power<TDenominator, TExponent>> Per<TDenominator, TExponent>(in Power<TDenominator, TExponent> denominator)
        where TDenominator : IUnit, IDimension
        where TExponent : INumber, IPositive => new(in this.factory.Divide(denominator.Factory));

    internal Quantity Create(in Double value) => new(in value, in this.factory.Create());

    internal Quantity Transform(in Quantity other) => other.Project(in this.factory.Create());
}

public readonly struct Power<TUnit, TExponent> : ITimes<Power<TUnit, TExponent>>, IPer<Power<TUnit, TExponent>>
    where TUnit : IUnit, IDimension
    where TExponent : INumber
{
    private readonly Factory factory;
    internal Factory Factory => this.factory;

    internal Power(in Factory factory) => this.factory = factory.Power<TExponent>();
    public Product<Power<TUnit, TExponent>, TRight> Times<TRight>(in Scalar<TRight> rightTerm)
        where TRight : IUnit, IDimension => new(in this.factory.Multiply(rightTerm.Factory));

    public Product<Power<TUnit, TExponent>, Power<TRight, TRightExponent>> Times<TRight, TRightExponent>(in Power<TRight, TRightExponent> rightTerm)
        where TRight : IUnit, IDimension
        where TRightExponent : INumber, IPositive => new(in this.factory.Multiply(rightTerm.Factory));
    public Quotient<Power<TUnit, TExponent>, TDenominator> Per<TDenominator>(in Scalar<TDenominator> denominator)
        where TDenominator : IUnit, IDimension => new(in this.factory.Divide(denominator.Factory));

    public Quotient<Power<TUnit, TExponent>, Power<TDenominator, TDenominatorExponent>> Per<TDenominator, TDenominatorExponent>(in Power<TDenominator, TDenominatorExponent> denominator)
        where TDenominator : IUnit, IDimension
        where TDenominatorExponent : INumber, IPositive => new(in this.factory.Divide(denominator.Factory));

    internal Quantity Create(in Double value) => new(in value, in this.factory.Create());

    internal Quantity Transform(in Quantity other) => other.Project(in this.factory.Create());
}
