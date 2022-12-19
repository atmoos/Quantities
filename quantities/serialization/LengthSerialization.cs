using System.Text.Json;
using System.Text.Json.Serialization;
using Quantities.Quantities;

namespace Quantities.Serialization;

public class LengthSerialization : JsonConverter<Length>
{
    public override Length Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Length value, JsonSerializerOptions options)
    {
        var jsonWriter = new JsonWriter(writer);
        jsonWriter.Start("length");
        value.Quant.Write(jsonWriter);
        jsonWriter.End();
    }
}
