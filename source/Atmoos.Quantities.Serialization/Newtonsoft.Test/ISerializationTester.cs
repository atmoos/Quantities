namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

internal interface ISerializationTester<TQuantity>
{
    void SupportsSerialization(TQuantity quantity);
    static abstract TheoryData<TQuantity> Quantities();
}
