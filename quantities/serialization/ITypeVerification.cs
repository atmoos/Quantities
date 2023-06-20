using Quantities.Dimensions;

namespace Quantities.Serialization;

internal interface ITypeVerification
{
    Type Verify(Type unit);
}

internal sealed class ScalarVerification : ITypeVerification
{
    private readonly Type dimension;
    public ScalarVerification(Type dimension) => this.dimension = dimension;
    public Type Verify(Type unit) => unit.IsAssignableTo(this.dimension) ? unit : throw Error(unit);
    private Exception Error(Type unit)
    {
        var interfaceType = unit.MostDerivedOf<IDimension>();
        return new InvalidOperationException($"Dimension mismatch: the unit '{unit.Name}' is not of dimension '{this.dimension.Name}', but of '{interfaceType.Name}'.");
    }
}
