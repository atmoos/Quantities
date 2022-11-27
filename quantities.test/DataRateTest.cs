using Quantities.Units.Si.Metric;

using Bytes = Quantities.Units.Si.Metric.Byte;

namespace Quantities.Test;

public class DataRateTest
{
    private const Double kibi = 1024;
    private const Double kilo = 1000;
    private const Double mebi = kibi * kibi;
    private const Double mega = kilo * kilo;
    private const Double gibi = kibi * mebi;

    [Fact]
    public void BitPerSecondToString() => FormattingMatches(v => DataRate.Metric<Bit>(v).PerSecond(), "bit/s");
    [Fact]
    public void BytePerSecondToString() => FormattingMatches(v => DataRate.Metric<Bytes>(v).PerSecond(), "B/s");
    [Fact]
    public void NibblePerSecondToString() => FormattingMatches(v => DataRate.Metric<Nibble>(v).PerSecond(), "N/s");
    [Fact]
    public void KibiBitPerSecondToString() => FormattingMatches(v => DataRate.Metric<Kibi, Bit>(v).PerSecond(), "Kibit/s");
    [Fact]
    public void KiloBitPerSecondToString() => FormattingMatches(v => DataRate.Metric<Kilo, Bit>(v).PerSecond(), "Kbit/s");
    [Fact]
    public void KiloBytePerSecondToString() => FormattingMatches(v => DataRate.Metric<Kilo, Bytes>(v).PerSecond(), "KB/s");
    [Fact]
    public void KibiBytePerSecondToString() => FormattingMatches(v => DataRate.Metric<Kibi, Bytes>(v).PerSecond(), "KiB/s");
    [Fact]
    public void KibiBytePerSecondToKiloBytePerSecond()
    {
        DataRate expected = DataRate.Metric<Kilo, Bytes>(12 * kibi / 1e3).PerSecond();
        DataRate rate = DataRate.Metric<Kibi, Bytes>(12).PerSecond();

        DataRate actual = rate.ToMetric<Kilo, Bytes>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void KiloBytePerHourToBytePerSecond()
    {
        DataRate expected = DataRate.Metric<Bytes>(10).PerSecond();
        DataRate speed = DataRate.Metric<Kilo, Bytes>(36).Per<Hour>();

        DataRate actual = speed.ToMetric<Bytes>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void MebiBytePerHourToKiloBytePerSecond()
    {
        DataRate expected = DataRate.Metric<Kilo, Bytes>(mebi / kilo).PerSecond();
        DataRate speed = DataRate.Metric<Mebi, Bytes>(3600).Per<Hour>();

        DataRate actual = speed.ToMetric<Kilo, Bytes>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void MebiBitPerHourToKiloBytePerSecond()
    {
        DataRate expected = DataRate.Metric<Kilo, Bytes>(mebi / kilo).PerSecond();
        DataRate speed = DataRate.Metric<Mebi, Bit>(3600 * 8).Per<Hour>();

        DataRate actual = speed.ToMetric<Kilo, Bytes>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void GibiBitPerHourToKiloBytePerMinute()
    {
        DataRate expected = DataRate.Metric<Kilo, Bytes>(gibi / kilo).Per<Minute>();
        DataRate speed = DataRate.Metric<Gibi, Bit>(60 * 8).Per<Hour>();

        DataRate actual = speed.ToMetric<Kilo, Bytes>().Per<Minute>();

        actual.Matches(expected);
    }

    [Fact]
    public void KiloBytePerMinuteToMebiBitPerHour()
    {
        DataRate expected = DataRate.Metric<Mebi, Bit>(60 * 8).Per<Hour>();
        DataRate speed = DataRate.Metric<Kilo, Bytes>(mebi / kilo).Per<Minute>();

        DataRate actual = speed.ToMetric<Mebi, Bit>().Per<Hour>();

        actual.Matches(expected);
    }

    [Fact]
    public void KiloBytePerSecondToKiloBitPerSecond()
    {
        DataRate expected = DataRate.Metric<Kilo, Bit>(16).PerSecond();
        DataRate speed = DataRate.Metric<Kilo, Bytes>(2).PerSecond();

        DataRate actual = speed.ToMetric<Kilo, Bit>().PerSecond();

        actual.Matches(expected);
    }
}
