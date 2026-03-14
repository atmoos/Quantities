using System.Globalization;
using Atmoos.Quantities.Core.Construction;
using Newtonsoft.Json;

namespace Atmoos.Quantities.Serialization.Newtonsoft.Test;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class DeserializerTest
{
    [Fact]
    public void MoveNextFindsRequestedToken()
    {
        using var text = new StringReader("{\"value\":1.2}");
        using var reader = new JsonTextReader(text);

        Boolean actual = reader.MoveNext(JsonToken.Float);

        Assert.True(actual);
        Assert.Equal(JsonToken.Float, reader.TokenType);
    }

    [Fact]
    public void MoveNextReturnsFalseIfTokenTypeIsMissing()
    {
        using var text = new StringReader("{\"value\":\"abc\"}");
        using var reader = new JsonTextReader(text);

        Boolean actual = reader.MoveNext(JsonToken.Float);

        Assert.False(actual);
    }

    [Fact]
    public void ReadNameOfReturnsPropertyName()
    {
        using var text = new StringReader("{\"quantity\":\"length\"}");
        using var reader = new JsonTextReader(text);

        String actual = reader.ReadNameOf(JsonToken.PropertyName);

        Assert.Equal("quantity", actual);
    }

    [Fact]
    public void ReadNameOfThrowsWhenTokenIsMissing()
    {
        using var text = new StringReader("[1,2,3]");
        using var reader = new JsonTextReader(text);

        Assert.Throws<InvalidDataException>(() => reader.ReadNameOf(JsonToken.PropertyName));
    }

    [Fact]
    public void ReadNumberReturnsDoubleForFloatToken()
    {
        using var text = new StringReader("{\"value\":1.2}");
        using var reader = new JsonTextReader(text);

        Double actual = reader.ReadNumber();

        Assert.Equal(1.2, actual, 6);
    }

    [Fact]
    public void ReadNumberThrowsIfNoFloatTokenExists()
    {
        using var text = new StringReader("{\"value\":\"1.2\"}");
        using var reader = new JsonTextReader(text);

        Assert.Throws<InvalidDataException>(() => reader.ReadNumber());
    }

    [Fact]
    public void ReadNumberThrowsIfFloatValueIsNotDouble()
    {
        using var text = new StringReader("{\"value\":1.2}");
        using var reader = new JsonTextReader(text) { FloatParseHandling = FloatParseHandling.Decimal, Culture = CultureInfo.InvariantCulture };

        var exception = Assert.Throws<InvalidDataException>(() => reader.ReadNumber());

        Assert.Equal("Expected a non null value for a Double.", exception.Message);
    }

    [Fact]
    public void ReadStringReturnsStringTokenValue()
    {
        using var text = new StringReader("{\"unit\":\"m\"}");
        using var reader = new JsonTextReader(text);

        String actual = reader.ReadString();

        Assert.Equal("m", actual);
    }

    [Fact]
    public void ReadStringThrowsWhenStringTokenIsMissing()
    {
        using var text = new StringReader("{\"unit\":1}");
        using var reader = new JsonTextReader(text);

        Assert.Throws<InvalidDataException>(() => reader.ReadString());
    }

    [Fact]
    public void UnwindToWalksBackToRequestedDepth()
    {
        using var text = new StringReader("{\"a\":{\"b\":1}}");
        using var reader = new JsonTextReader(text);
        while (reader.Read() && reader.TokenType != JsonToken.Integer) { }

        reader.UnwindTo(0);

        Assert.True(reader.Depth <= 0);
        Assert.Equal(JsonToken.EndObject, reader.TokenType);
    }

    [Fact]
    public void ReadBuildsQuantityModelWithDefaultExponent()
    {
        using var text = new StringReader("{\"prefix\":\"m\",\"unit\":\"s\"}");
        using var reader = new JsonTextReader(text);
        JsonSerializer serializer = new();

        QuantityModel actual = serializer.Read(reader, "si");

        Assert.Equal("si", actual.System);
        Assert.Equal("m", actual.Prefix);
        Assert.Equal("s", actual.Unit);
        Assert.Equal(1, actual.Exponent);
    }

    [Fact]
    public void ReadBuildsQuantityModelWithExplicitExponent()
    {
        using var text = new StringReader("{\"exponent\":-1,\"unit\":\"h\"}");
        using var reader = new JsonTextReader(text);
        JsonSerializer serializer = new();

        QuantityModel actual = serializer.Read(reader, "metric");

        Assert.Equal("metric", actual.System);
        Assert.Equal(-1, actual.Exponent);
        Assert.Equal("h", actual.Unit);
        Assert.Null(actual.Prefix);
    }
}
