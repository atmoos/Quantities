using System.Numerics;
using Atmoos.Quantities.Core.Numerics;
using Atmoos.Quantities.Creation;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units;

namespace Atmoos.Quantities;

public readonly struct Pressure : IQuantity<Pressure>, IPressure
    , IQuotient<Pressure, IPressure, IForce, ILength, Two>
    , IMultiplyOperators<Pressure, Area, Force>
{
    private readonly Quantity pressure;
    internal Quantity Value => this.pressure;
    Quantity IQuantity<Pressure>.Value => this.pressure;
    internal Pressure(in Quantity value) => this.pressure = value;
    public Pressure To<TUnit>(in Scalar<TUnit> other)
        where TUnit : IPressure, IUnit => new(other.Transform(in this.pressure));
    public Pressure To<TNominator, TDenominator>(in Quotient<TNominator, Power<TDenominator, Two>> other)
        where TNominator : IForce, IUnit
        where TDenominator : ILength, IUnit => new(other.Transform(in this.pressure));
    public static Pressure Of<TUnit>(in Double value, in Scalar<TUnit> measure)
        where TUnit : IPressure, IUnit => new(measure.Create(in value));
    public static Pressure Of<TForce, TLength>(in Double value, in Quotient<TForce, Power<TLength, Two>> measure)
        where TForce : IUnit, IForce where TLength : IUnit, ILength => new(measure.Create(in value));
    static Pressure IFactory<Pressure>.Create(in Quantity value) => new(in value);
    internal static Pressure From(in Force force, in Area area) => new(force.Value / area.Value);
    public Boolean Equals(Pressure other) => this.pressure.Equals(other.pressure);
    public override Boolean Equals(Object? obj) => obj is Pressure pressure && Equals(pressure);
    public override Int32 GetHashCode() => this.pressure.GetHashCode();
    public override String ToString() => this.pressure.ToString();
    public String ToString(String? format, IFormatProvider? provider) => this.pressure.ToString(format, provider);

    public static implicit operator Double(Pressure pressure) => pressure.pressure;
    public static Boolean operator ==(Pressure left, Pressure right) => left.Equals(right);
    public static Boolean operator !=(Pressure left, Pressure right) => !left.Equals(right);
    public static Boolean operator >(Pressure left, Pressure right) => left.pressure > right.pressure;
    public static Boolean operator >=(Pressure left, Pressure right) => left.pressure >= right.pressure;
    public static Boolean operator <(Pressure left, Pressure right) => left.pressure < right.pressure;
    public static Boolean operator <=(Pressure left, Pressure right) => left.pressure <= right.pressure;
    public static Pressure operator +(Pressure left, Pressure right) => new(left.pressure + right.pressure);
    public static Pressure operator -(Pressure left, Pressure right) => new(left.pressure - right.pressure);
    public static Pressure operator *(Double scalar, Pressure right) => new(scalar * right.pressure);
    public static Pressure operator *(Pressure left, Double scalar) => new(scalar * left.pressure);
    public static Pressure operator /(Pressure left, Double scalar) => new(left.pressure / scalar);
    public static Double operator /(Pressure left, Pressure right) => left.pressure.Ratio(in right.pressure);

    public static Force operator *(Pressure pressure, Area area) => Force.From(in pressure, in area);
}
