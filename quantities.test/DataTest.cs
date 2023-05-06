using Quantities.Units.Si.Metric;

using Bytes = Quantities.Units.Si.Metric.Byte;

namespace Quantities.Test;

public class DataTest
{
    private const Double kibi = 1024;
    private const Double mebi = kibi * kibi;
    private const Double gibi = kibi * mebi;

    [Fact]
    public void BitToString() => FormattingMatches(v => Data.In<Bit>(v), "bit");
    [Fact]
    public void ByteToString() => FormattingMatches(v => Data.In<Bytes>(v), "B");
    [Fact]
    public void NibbleToString() => FormattingMatches(v => Data.In<Nibble>(v), "N");
    [Fact]
    public void KibiBitToString() => FormattingMatches(v => Data.In<Kibi, Bit>(v), "Kibit");
    [Fact]
    public void KiloBitToString() => FormattingMatches(v => Data.In<Kilo, Bit>(v), "Kbit");
    [Fact]
    public void KiloByteToString() => FormattingMatches(v => Data.In<Kilo, Bytes>(v), "KB");
    [Fact]
    public void KibiByteToString() => FormattingMatches(v => Data.In<Kibi, Bytes>(v), "KiB");
    [Fact]
    public void DefinitionOfNibble()
    {
        Data definition = Data.In<Bit>(4);
        Data oneNibble = Data.In<Nibble>(1);

        Assert.Equal(definition, oneNibble);
    }
    [Fact]
    public void DefinitionOfByte()
    {
        Data definition = Data.In<Bit>(8);
        Data oneByte = Data.In<Bytes>(1);

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void DefinitionOfKiloByte()
    {
        Data definition = Data.In<Bit>(8 * 1000);
        Data oneByte = Data.In<Kilo, Bytes>(1);

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void DefinitionOfKibiByte()
    {
        Data definition = Data.In<Bit>(8 * kibi);
        Data oneByte = Data.In<Kibi, Bytes>(1);

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void MebiByteToMegaByte()
    {
        Data expected = Data.In<Mega, Bytes>(3 * mebi / 1e6);
        Data mebiBytes = Data.In<Mebi, Bytes>(3);

        Data actual = mebiBytes.To<Mega, Bytes>();

        actual.Matches(expected);
    }
    [Fact]
    public void GigaByteToGibiByte()
    {
        Data expected = Data.In<Gibi, Bytes>(22 * 1e9 / gibi);
        Data mebiBytes = Data.In<Giga, Bytes>(22);

        Data actual = mebiBytes.To<Gibi, Bytes>();

        actual.Matches(expected);
    }
    [Fact]
    public void DataRateTimesTimeIsAmountOfData()
    {
        Time time = Time.Of(12).Si<Milli, Second>();
        DataRate rate = DataRate.In<Mega, Bit>(32).PerSecond();
        // Note that the units aren't preserved yet...
        Data expected = Data.In<Kibi, Bytes>(12 * 32 / (8 * 1e3) * 1e6 / kibi);

        Data actual = rate * time;

        actual.Matches(expected);
    }
}
