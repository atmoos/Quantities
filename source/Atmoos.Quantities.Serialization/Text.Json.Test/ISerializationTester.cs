namespace Quantities.Serialization.Text.Json.Test;

internal interface ISerializationTester<TQuantity>
{
    void SupportsSerialization(TQuantity quantity);
    static abstract IEnumerable<Object[]> Quantities();
}

internal interface IInjectedUnitTester<TQuantity>
{
    void DeserializationSupportsInjectedUnits(TQuantity quantity);
}
