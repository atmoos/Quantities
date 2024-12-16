namespace Atmoos.Quantities.Serialization;

internal interface IBuilder
{
    Quantity Build(in Double value);
}

internal sealed class Builder<TMeasure> : IBuilder
    where TMeasure : IMeasure
{
    public Quantity Build(in Double value) => Quantity.Of<TMeasure>(in value);
}
