using Quantities.Units.Si.Metric;
using Quantities.Units.Si.Metric.UnitsOfInformation;

using Bytes = Quantities.Units.Si.Metric.UnitsOfInformation.Byte;

namespace Quantities.Test;

public class DataRateTest
{
    private const Double kibi = 1024;
    private const Double kilo = 1000;
    private const Double mebi = kibi * kibi;
    private const Double gibi = kibi * mebi;

    [Fact]
    public void BitPerSecondToString() => FormattingMatches(v => DataRate.Of(v, Metric<Bit>().Per(Si<Second>())), "bit/s");
    [Fact]
    public void BytePerSecondToString() => FormattingMatches(v => DataRate.Of(v, Metric<Bytes>().Per(Si<Second>())), "B/s");
    [Fact]
    public void NibblePerSecondToString() => FormattingMatches(v => DataRate.Of(v, Metric<Nibble>().Per(Si<Second>())), "N/s");
    [Fact]
    public void KibiBitPerSecondToString() => FormattingMatches(v => DataRate.Of(v, Binary<Kibi, Bit>().Per(Si<Second>())), "Kibit/s");
    [Fact]
    public void KiloBitPerSecondToString() => FormattingMatches(v => DataRate.Of(v, Metric<Kilo, Bit>().Per(Si<Second>())), "Kbit/s");
    [Fact]
    public void KiloBytePerSecondToString() => FormattingMatches(v => DataRate.Of(v, Metric<Kilo, Bytes>().Per(Si<Second>())), "KB/s");
    [Fact]
    public void KibiBytePerSecondToString() => FormattingMatches(v => DataRate.Of(v, Binary<Kibi, Bytes>().Per(Si<Second>())), "KiB/s");
    [Fact]
    public void KibiBytePerSecondToKiloBytePerSecond()
    {
        DataRate expected = DataRate.Of(12 * kibi / 1e3, Metric<Kilo, Bytes>().Per(Si<Second>()));
        DataRate rate = DataRate.Of(12, Binary<Kibi, Bytes>().Per(Si<Second>()));

        DataRate actual = rate.To(Metric<Kilo, Bytes>().Per(Si<Second>()));

        actual.Matches(expected);
    }
    [Fact]
    public void KiloBytePerHourToBytePerSecond()
    {
        DataRate expected = DataRate.Of(10, Metric<Bytes>().Per(Si<Second>()));
        DataRate speed = DataRate.Of(36, Metric<Kilo, Bytes>().Per(Metric<Hour>()));

        DataRate actual = speed.To(Metric<Bytes>().Per(Si<Second>()));

        actual.Matches(expected);
    }
    [Fact]
    public void MebiBytePerHourToKiloBytePerSecond()
    {
        DataRate expected = DataRate.Of(mebi / kilo, Metric<Kilo, Bytes>().Per(Si<Second>()));
        DataRate speed = DataRate.Of(3600, Binary<Mebi, Bytes>().Per(Metric<Hour>()));

        DataRate actual = speed.To(Metric<Kilo, Bytes>().Per(Si<Second>()));

        actual.Matches(expected);
    }
    [Fact]
    public void MebiBitPerHourToKiloBytePerSecond()
    {
        DataRate expected = DataRate.Of(mebi / kilo, Metric<Kilo, Bytes>().Per(Si<Second>()));
        DataRate speed = DataRate.Of(3600 * 8, Binary<Mebi, Bit>().Per(Metric<Hour>()));

        DataRate actual = speed.To(Metric<Kilo, Bytes>().Per(Si<Second>()));

        actual.Matches(expected);
    }
    [Fact]
    public void GibiBitPerHourToKiloBytePerMinute()
    {
        DataRate expected = DataRate.Of(gibi / kilo, Metric<Kilo, Bytes>().Per(Metric<Minute>()));
        DataRate speed = DataRate.Of(60 * 8, Binary<Gibi, Bit>().Per(Metric<Hour>()));

        DataRate actual = speed.To(Metric<Kilo, Bytes>().Per(Metric<Minute>()));

        actual.Matches(expected);
    }

    [Fact]
    public void KiloBytePerMinuteToMebiBitPerHour()
    {
        DataRate expected = DataRate.Of(60 * 8, Binary<Mebi, Bit>().Per(Metric<Hour>()));
        DataRate speed = DataRate.Of(mebi / kilo, Metric<Kilo, Bytes>().Per(Metric<Minute>()));

        DataRate actual = speed.To(Binary<Mebi, Bit>().Per(Metric<Hour>()));

        actual.Matches(expected);
    }

    [Fact]
    public void KiloBytePerSecondToKiloBitPerSecond()
    {
        DataRate expected = DataRate.Of(16, Metric<Kilo, Bit>().Per(Si<Second>()));
        DataRate speed = DataRate.Of(2, Metric<Kilo, Bytes>().Per(Si<Second>()));

        DataRate actual = speed.To(Metric<Kilo, Bit>().Per(Si<Second>()));

        actual.Matches(expected);
    }
    [Fact]
    public void DataDividedByTimeIsDataRate()
    {
        Time time = Time.Of(12, Si<Micro, Second>());
        Data data = Data.Of(24, Binary<Gibi, Nibble>());
        DataRate expected = DataRate.Of(24 / 12, Binary<Gibi, Nibble>().Per(Si<Micro, Second>()));

        DataRate actual = data / time;

        actual.Matches(expected);
    }
}
