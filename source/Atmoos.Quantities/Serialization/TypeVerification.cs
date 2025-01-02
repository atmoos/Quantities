using Atmoos.Quantities.Dimensions;

namespace Atmoos.Quantities.Serialization;

public readonly record struct QuantityModel(String System, Int32 Exponent, String? Prefix, String Unit);

internal readonly struct TypeVerification(Type dimension)
{
    public Type Verify(Type unit) => unit.IsAssignableTo(dimension) ? unit : throw Error(unit);
    private InvalidOperationException Error(Type unit)
    {
        var interfaceType = unit.MostDerivedOf(typeof(IDimension));
        return new($"Dimension mismatch: the unit '{unit.Name}' is not of dimension '{dimension.Name}', but of '{interfaceType.Name}'.");
    }
}
