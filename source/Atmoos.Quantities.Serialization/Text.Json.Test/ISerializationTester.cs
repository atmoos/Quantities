namespace Atmoos.Quantities.Serialization.Text.Json.Test;

internal interface ISerializationTester<TQuantity>
{
    void SupportsSerialization(TQuantity quantity);
    static abstract TheoryData<TQuantity> Quantities();
}

internal interface IInjectedUnitTester<TQuantity>
{
    void DeserializationSupportsInjectedUnits(TQuantity quantity);
}
