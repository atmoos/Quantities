using Quantities.Units.Si.Metric;

using Bytes = Quantities.Units.Si.Metric.Byte;

namespace Quantities.Test;

public class DataTest
{
    private const Double kibi = 1024;
    private const Double mebi = kibi * kibi;
    private const Double gibi = kibi * mebi;

    [Fact]
    public void BitToString() => FormattingMatches(v => Data.Metric<Bit>(v), "bit");
    [Fact]
    public void ByteToString() => FormattingMatches(v => Data.Metric<Bytes>(v), "B");
    [Fact]
    public void NibbleToString() => FormattingMatches(v => Data.Metric<Nibble>(v), "N");
    [Fact]
    public void KibiBitToString() => FormattingMatches(v => Data.Metric<Kibi, Bit>(v), "Kibit");
    [Fact]
    public void KiloBitToString() => FormattingMatches(v => Data.Metric<Kilo, Bit>(v), "Kbit");
    [Fact]
    public void KiloByteToString() => FormattingMatches(v => Data.Metric<Kilo, Bytes>(v), "KB");
    [Fact]
    public void KibiByteToString() => FormattingMatches(v => Data.Metric<Kibi, Bytes>(v), "KiB");
    [Fact]
    public void DefinitionOfNibble()
    {
        Data definition = Data.Metric<Bit>(4);
        Data oneNibble = Data.Metric<Nibble>(1);

        Assert.Equal(definition, oneNibble);
    }
    [Fact]
    public void DefinitionOfByte()
    {
        Data definition = Data.Metric<Bit>(8);
        Data oneByte = Data.Metric<Bytes>(1);

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void DefinitionOfKiloByte()
    {
        Data definition = Data.Metric<Bit>(8 * 1000);
        Data oneByte = Data.Metric<Kilo, Bytes>(1);

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void DefinitionOfKibiByte()
    {
        Data definition = Data.Metric<Bit>(8 * kibi);
        Data oneByte = Data.Metric<Kibi, Bytes>(1);

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void MebiByteToMegaByte()
    {
        Data expected = Data.Metric<Mega, Bytes>(3 * mebi / 1e6);
        Data mebiBytes = Data.Metric<Mebi, Bytes>(3);

        Data actual = mebiBytes.ToMetric<Mega, Bytes>();

        actual.Matches(expected);
    }
    [Fact]
    public void GigaByteToGibiByte()
    {
        Data expected = Data.Metric<Gibi, Bytes>(22 * 1e9 / gibi);
        Data mebiBytes = Data.Metric<Giga, Bytes>(22);

        Data actual = mebiBytes.ToMetric<Gibi, Bytes>();

        actual.Matches(expected);
    }
    [Fact]
    public void DataRateTimesTimeIsAmountOfData()
    {
        Time time = Time.Si<Milli, Second>(12);
        DataRate rate = DataRate.Metric<Mega, Bit>(32).PerSecond();
        // Note that the units aren't preserved yet...
        Data expected = Data.Metric<Kibi, Bytes>(12 * 32 / (8 * 1e3) * 1e6 / kibi);

        Data actual = rate * time;

        actual.Matches(expected);
    }
}
