using Xunit;
using Quantities.Unit.Si;
using Quantities.Unit.Si.Derived;
using Quantities.Prefixes;

using static Quantities.Test.Metrics;

namespace Quantities.Test
{
    public sealed class ElectricalResistanceTest
    {
        [Fact]
        public void OhmToString() => FormattingMatches(v => ElectricalResistance.Si<Ohm>(v), "Ω");
        [Fact]
        public void KiloOhmToString() => FormattingMatches(v => ElectricalResistance.Si<Kilo, Ohm>(v), "KΩ");
        [Fact]
        public void MilliOhmToString() => FormattingMatches(v => ElectricalResistance.Si<Milli, Ohm>(v), "mΩ");
        [Fact]
        public void OhmsLawInBaseUnits()
        {
            var volts = ElectricPotential.Si<Volt>(12);
            var ampere = ElectricCurrent.Si<Ampere>(3);
            var expected = ElectricalResistance.Si<Ohm>(4);

            var resistance = volts / ampere;

            resistance.Matches(expected);
        }
        [Fact]
        public void OhmsLawInPrefixedUnits()
        {
            var volts = ElectricPotential.Si<Milli, Volt>(12);
            var ampere = ElectricCurrent.Si<Micro, Ampere>(3);
            var expected = ElectricalResistance.Si<Kilo, Ohm>(4);

            var resistance = volts / ampere;

            resistance.Matches(expected);
        }
    }
}