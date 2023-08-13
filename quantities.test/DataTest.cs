using Quantities.Units.Si.Metric;

using Bytes = Quantities.Units.Si.Metric.Byte;

namespace Quantities.Test;

public class DataTest
{
    private const Double kibi = 1024;
    private const Double mebi = kibi * kibi;
    private const Double gibi = kibi * mebi;

    [Fact]
    public void BitToString() => FormattingMatches(v => Data.Of(v).Metric<Bit>(), "bit");
    [Fact]
    public void ByteToString() => FormattingMatches(v => Data.Of(v).Metric<Bytes>(), "B");
    [Fact]
    public void NibbleToString() => FormattingMatches(v => Data.Of(v).Metric<Nibble>(), "N");
    [Fact]
    public void KibiBitToString() => FormattingMatches(v => Data.Of(v).Binary<Kibi, Bit>(), "Kibit");
    [Fact]
    public void KiloBitToString() => FormattingMatches(v => Data.Of(v).Metric<Kilo, Bit>(), "Kbit");
    [Fact]
    public void KiloByteToString() => FormattingMatches(v => Data.Of(v).Metric<Kilo, Bytes>(), "KB");
    [Fact]
    public void KibiByteToString() => FormattingMatches(v => Data.Of(v).Binary<Kibi, Bytes>(), "KiB");
    [Fact]
    public void DefinitionOfNibble()
    {
        Data definition = Data.Of(4).Metric<Bit>();
        Data oneNibble = Data.Of(1).Metric<Nibble>();

        Assert.Equal(definition, oneNibble);
    }
    [Fact]
    public void DefinitionOfByte()
    {
        Data definition = Data.Of(8).Metric<Bit>();
        Data oneByte = Data.Of(1).Metric<Bytes>();

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void DefinitionOfKiloByte()
    {
        Data definition = Data.Of(8 * 1000).Metric<Bit>();
        Data oneByte = Data.Of(1).Metric<Kilo, Bytes>();

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void DefinitionOfKibiByte()
    {
        Data definition = Data.Of(8 * kibi).Metric<Bit>();
        Data oneByte = Data.Of(1).Binary<Kibi, Bytes>();

        Assert.Equal(definition, oneByte);
    }
    [Fact]
    public void MebiByteToMegaByte()
    {
        Data expected = Data.Of(3 * mebi / 1e6).Metric<Mega, Bytes>();
        Data mebiBytes = Data.Of(3).Binary<Mebi, Bytes>();

        Data actual = mebiBytes.To.Metric<Mega, Bytes>();

        actual.Matches(expected);
    }
    [Fact]
    public void GigaByteToGibiByte()
    {
        Data expected = Data.Of(22 * 1e9 / gibi).Binary<Gibi, Bytes>();
        Data mebiBytes = Data.Of(22).Metric<Giga, Bytes>();

        Data actual = mebiBytes.To.Binary<Gibi, Bytes>();

        actual.Matches(expected);
    }
    [Fact]
    public void DataRateTimesTimeIsAmountOfData()
    {
        Time time = Time.Of(12).Si<Milli, Second>();
        DataRate rate = DataRate.Of(32).Metric<Mega, Bit>().Per.Si<Second>();
        Data expected = Data.Of(32d * 12 / 1000).Metric<Mega, Bit>();

        Data actual = rate * time;

        actual.Matches(expected);
    }
}
