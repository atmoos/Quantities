namespace Quantities.Serialization.Text.Json.Text;

internal interface ISerializationTester<TQuantity>
{
    void SupportsSerialization(TQuantity quantity);
    static abstract IEnumerable<Object[]> Quantities();
}
