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
    public void BitPerSecondToString() => FormattingMatches(v => DataRate.In<Bit>(v).PerSecond(), "bit/s");
    [Fact]
    public void BytePerSecondToString() => FormattingMatches(v => DataRate.In<Bytes>(v).PerSecond(), "B/s");
    [Fact]
    public void NibblePerSecondToString() => FormattingMatches(v => DataRate.In<Nibble>(v).PerSecond(), "N/s");
    [Fact]
    public void KibiBitPerSecondToString() => FormattingMatches(v => DataRate.In<Kibi, Bit>(v).PerSecond(), "Kibit/s");
    [Fact]
    public void KiloBitPerSecondToString() => FormattingMatches(v => DataRate.In<Kilo, Bit>(v).PerSecond(), "Kbit/s");
    [Fact]
    public void KiloBytePerSecondToString() => FormattingMatches(v => DataRate.In<Kilo, Bytes>(v).PerSecond(), "KB/s");
    [Fact]
    public void KibiBytePerSecondToString() => FormattingMatches(v => DataRate.In<Kibi, Bytes>(v).PerSecond(), "KiB/s");
    [Fact]
    public void KibiBytePerSecondToKiloBytePerSecond()
    {
        DataRate expected = DataRate.In<Kilo, Bytes>(12 * kibi / 1e3).PerSecond();
        DataRate rate = DataRate.In<Kibi, Bytes>(12).PerSecond();

        DataRate actual = rate.To<Kilo, Bytes>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void KiloBytePerHourToBytePerSecond()
    {
        DataRate expected = DataRate.In<Bytes>(10).PerSecond();
        DataRate speed = DataRate.In<Kilo, Bytes>(36).Per<Hour>();

        DataRate actual = speed.To<Bytes>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void MebiBytePerHourToKiloBytePerSecond()
    {
        DataRate expected = DataRate.In<Kilo, Bytes>(mebi / kilo).PerSecond();
        DataRate speed = DataRate.In<Mebi, Bytes>(3600).Per<Hour>();

        DataRate actual = speed.To<Kilo, Bytes>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void MebiBitPerHourToKiloBytePerSecond()
    {
        DataRate expected = DataRate.In<Kilo, Bytes>(mebi / kilo).PerSecond();
        DataRate speed = DataRate.In<Mebi, Bit>(3600 * 8).Per<Hour>();

        DataRate actual = speed.To<Kilo, Bytes>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void GibiBitPerHourToKiloBytePerMinute()
    {
        DataRate expected = DataRate.In<Kilo, Bytes>(gibi / kilo).Per<Minute>();
        DataRate speed = DataRate.In<Gibi, Bit>(60 * 8).Per<Hour>();

        DataRate actual = speed.To<Kilo, Bytes>().Per<Minute>();

        actual.Matches(expected);
    }

    [Fact]
    public void KiloBytePerMinuteToMebiBitPerHour()
    {
        DataRate expected = DataRate.In<Mebi, Bit>(60 * 8).Per<Hour>();
        DataRate speed = DataRate.In<Kilo, Bytes>(mebi / kilo).Per<Minute>();

        DataRate actual = speed.To<Mebi, Bit>().Per<Hour>();

        actual.Matches(expected);
    }

    [Fact]
    public void KiloBytePerSecondToKiloBitPerSecond()
    {
        DataRate expected = DataRate.In<Kilo, Bit>(16).PerSecond();
        DataRate speed = DataRate.In<Kilo, Bytes>(2).PerSecond();

        DataRate actual = speed.To<Kilo, Bit>().PerSecond();

        actual.Matches(expected);
    }
    [Fact]
    public void DataDividedByTimeIsDataRate()
    {
        Time time = Time.Of(12).Si<Micro, Second>();
        Data data = Data.In<Gibi, Nibble>(24);
        DataRate expected = DataRate.In<Gibi, Nibble>(24 / 12).Per<Micro, Second>();

        DataRate actual = data / time;

        actual.Matches(expected);
    }
}
