using UnitsNet;
using Quantities.Units.Si.Metric;
using Quantities.Units.Si.Metric.UnitsOfInformation;
using Quantities.Units.Si.Derived;
using Quantities.Units.Imperial.Temperature;
using Quantities.Units.NonStandard.Temperature;

using nLength = UnitsNet.Length;
using nTemperature = UnitsNet.Temperature;
using Bytes = Quantities.Units.Si.Metric.UnitsOfInformation.Byte;

namespace Quantities.Test.Compare;

public class QuantitiesAndUnitsNetAreEquallyAccurate
{
    [Fact]
    public void ÅngströmToNanoMetre()
    {
        const Double expectedNanoMetre = 1d;

        // Quantities
        Length ångström = Length.Of(10, Metric<Ångström>());
        Length nanoMetres = ångström.To(Si<Nano, Metre>());

        // UnitsNet
        nLength nÅngström = nLength.FromAngstroms(10);
        nLength nNanoMetre = nÅngström.ToUnit(UnitsNet.Units.LengthUnit.Nanometer);

        // Equally precise
        Assert.Equal(expectedNanoMetre, nanoMetres);
        Assert.Equal(expectedNanoMetre, nNanoMetre.Value);
    }

    [Fact]
    public void GibiBitPerHourToKiloBytePerMinute()
    {
        const Double expectedRate = 1024d * 1024d * 1024d / 1000d;

        // Quantities
        DataRate speed = DataRate.Of(8, Binary<Gibi, Bit>().Per(Si<Second>()));
        DataRate actual = speed.To(Metric<Kilo, Bytes>().Per(Si<Second>()));

        // UnitsNet
        BitRate nSpeed = BitRate.FromGibibitsPerSecond(8);
        BitRate nActual = nSpeed.ToUnit(UnitsNet.Units.BitRateUnit.KilobytePerSecond);

        // Equally precise
        Assert.Equal(expectedRate, actual);
        Assert.Equal(expectedRate, (Double)nActual.Value); // but Value is a Decimal here :-/
    }

    [Fact]
    public void CelsiusToFahrenheit()
    {
        const Double expectedFahrenheit = 98.6;

        // Quantities
        Temperature temperature = Temperature.Of(37.0, Metric<Celsius>());
        Temperature actual = temperature.To(Imperial<Fahrenheit>());

        // UnitsNet
        nTemperature nTemperature = nTemperature.FromDegreesCelsius(37.0);
        nTemperature nActual = nTemperature.ToUnit(UnitsNet.Units.TemperatureUnit.DegreeFahrenheit);

        // Equally precise :-)
        Assert.Equal(expectedFahrenheit, actual);
        Assert.Equal(expectedFahrenheit, nActual.Value);
    }

    [Fact]
    public void KelvinToRømer()
    {
        const Double expectedRømer = -1.8345; // calculated using full precision math.

        // Quantities
        Temperature temperature = Temperature.Of(255.37, Si<Kelvin>());
        Temperature actual = temperature.To(NonStandard<Rømer>());

        // UnitsNet
        nTemperature nTemperature = nTemperature.FromKelvins(255.37);
        nTemperature nActual = nTemperature.ToUnit(UnitsNet.Units.TemperatureUnit.DegreeRoemer);

        // Quantities and UnitsNet are equally bad (measured by precision parameter only)
        Assert.Equal(expectedRømer, actual, 13);        // -1.8344999999999885
        Assert.Equal(expectedRømer, nActual.Value, 13); // -1.83449999999999
    }
}
