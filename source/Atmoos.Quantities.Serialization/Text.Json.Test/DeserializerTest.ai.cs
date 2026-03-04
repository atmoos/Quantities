using System.Text;
using System.Text.Json;
using Atmoos.Quantities.Core.Construction;

namespace Atmoos.Quantities.Serialization.Text.Json.Test;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class DeserializerTest
{
    [Fact]
    public void MoveNextFindsRequestedToken()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"value\":1.2}");
        var reader = new Utf8JsonReader(bytes);

        Boolean actual = reader.MoveNext(JsonTokenType.Number);

        Assert.True(actual);
        Assert.Equal(JsonTokenType.Number, reader.TokenType);
    }

    [Fact]
    public void MoveNextReturnsFalseIfTokenTypeIsMissing()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"value\":true}");
        var reader = new Utf8JsonReader(bytes);

        Boolean actual = reader.MoveNext(JsonTokenType.String);

        Assert.False(actual);
    }

    [Fact]
    public void ReadNameOfReturnsPropertyName()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"quantity\":\"length\"}");
        var reader = new Utf8JsonReader(bytes);

        String actual = reader.ReadNameOf(JsonTokenType.PropertyName);

        Assert.Equal("quantity", actual);
    }

    [Fact]
    public void ReadNameOfThrowsWhenTokenIsMissing()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("[1,2,3]");
        var reader = new Utf8JsonReader(bytes);

        InvalidDataException? exception = null;
        try {
            reader.ReadNameOf(JsonTokenType.PropertyName);
        }
        catch (InvalidDataException ex) {
            exception = ex;
        }

        Assert.NotNull(exception);
    }

    [Fact]
    public void ReadNumberReturnsDoubleForNumberToken()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"value\":1.2}");
        var reader = new Utf8JsonReader(bytes);

        Double actual = reader.ReadNumber();

        Assert.Equal(1.2, actual, 6);
    }

    [Fact]
    public void ReadNumberThrowsIfNumberTokenIsMissing()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"value\":\"1.2\"}");
        var reader = new Utf8JsonReader(bytes);

        InvalidDataException? exception = null;
        try {
            reader.ReadNumber();
        }
        catch (InvalidDataException ex) {
            exception = ex;
        }

        Assert.NotNull(exception);
    }

    [Fact]
    public void ReadStringReturnsStringTokenValue()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"unit\":\"m\"}");
        var reader = new Utf8JsonReader(bytes);

        String actual = reader.ReadString();

        Assert.Equal("m", actual);
    }

    [Fact]
    public void ReadStringThrowsIfStringTokenIsMissing()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"unit\":1}");
        var reader = new Utf8JsonReader(bytes);

        InvalidDataException? exception = null;
        try {
            reader.ReadString();
        }
        catch (InvalidDataException ex) {
            exception = ex;
        }

        Assert.NotNull(exception);
    }

    [Fact]
    public void UnwindToWalksBackToRequestedDepth()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"a\":{\"b\":1}}");
        var reader = new Utf8JsonReader(bytes);
        while (reader.Read() && reader.TokenType != JsonTokenType.Number) { }

        reader.UnwindTo(0);

        Assert.True(reader.CurrentDepth <= 0);
        Assert.Equal(JsonTokenType.EndObject, reader.TokenType);
    }

    [Fact]
    public void ReadBuildsQuantityModelWithDefaultExponent()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"prefix\":\"m\",\"unit\":\"s\"}");
        var reader = new Utf8JsonReader(bytes);

        QuantityModel actual = reader.Read("si");

        Assert.Equal("si", actual.System);
        Assert.Equal("m", actual.Prefix);
        Assert.Equal("s", actual.Unit);
        Assert.Equal(1, actual.Exponent);
    }

    [Fact]
    public void ReadBuildsQuantityModelWithExplicitExponent()
    {
        Byte[] bytes = Encoding.UTF8.GetBytes("{\"exponent\":-1,\"unit\":\"h\"}");
        var reader = new Utf8JsonReader(bytes);

        QuantityModel actual = reader.Read("metric");

        Assert.Equal("metric", actual.System);
        Assert.Equal(-1, actual.Exponent);
        Assert.Equal("h", actual.Unit);
        Assert.Null(actual.Prefix);
    }
}
