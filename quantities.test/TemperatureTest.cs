using Quantities.Units.Imperial.Temperature;
using Quantities.Units.NonStandard.Temperature;
using Quantities.Units.Si.Derived;
using Newton = Quantities.Units.NonStandard.Temperature.Newton;

namespace Quantities.Test;

// For a lot of the values here, see: https://en.wikipedia.org/wiki/Conversion_of_units_of_temperature#Comparison_of_temperature_scales
public sealed class TemperatureTest
{
    [Fact]
    public void KelvinToString() => FormattingMatches(v => Temperature.Of(v).Si<Kelvin>(), "K");
    [Fact]
    public void CelsiusToString() => FormattingMatches(v => Temperature.Of(v).Metric<Celsius>(), "°C");
    [Fact]
    public void DelisleToString() => FormattingMatches(v => Temperature.Of(v).NonStandard<Delisle>(), "°De");
    [Fact]
    public void NewtonToString() => FormattingMatches(v => Temperature.Of(v).NonStandard<Newton>(), "°N");
    [Fact]
    public void RéaumurToString() => FormattingMatches(v => Temperature.Of(v).NonStandard<Réaumur>(), "°Ré");
    [Fact]
    public void RømerToString() => FormattingMatches(v => Temperature.Of(v).NonStandard<Rømer>(), "°Rø");
    [Fact]
    public void FahrenheitToString() => FormattingMatches(v => Temperature.Of(v).Imperial<Fahrenheit>(), "°F");
    [Fact]
    public void RankineToString() => FormattingMatches(v => Temperature.Of(v).Imperial<Rankine>(), "°R");
    [Fact]
    public void GasMarkToString() => FormattingMatches(v => Temperature.Of(v).Imperial<GasMark>(), "GM");
    [Fact]
    public void AddThreeTemperatures()
    {
        Temperature a = Temperature.Of(-40).Metric<Celsius>();
        Temperature b = Temperature.Of(-40).Imperial<Fahrenheit>();
        Temperature c = Temperature.Of(363.15).Si<Kelvin>();
        Temperature expected = Temperature.Of(10).Metric<Celsius>();

        Temperature actual = a + b + c;

        actual.Matches(expected);
    }
    [Fact]
    public void SubtractTemperatures()
    {
        Temperature a = Temperature.Of(70).Metric<Celsius>();
        Temperature b = Temperature.Of(273.15 + 28).Si<Kelvin>();
        Temperature expected = Temperature.Of(42).Metric<Celsius>();

        Temperature actual = a - b;

        actual.Matches(expected);
    }

    [Fact]
    public void KelvinToCelsius()
    {
        Temperature temperature = Temperature.Of(36 + 273.15).Si<Kelvin>();
        Temperature expected = Temperature.Of(36).Metric<Celsius>();

        Temperature actual = temperature.To.Metric<Celsius>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToFahrenheit()
    {
        Temperature temperature = Temperature.Of(364).Si<Kelvin>();
        Temperature expected = Temperature.Of(195.53).Imperial<Fahrenheit>();

        Temperature actual = temperature.To.Imperial<Fahrenheit>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void CelsiusToKelvin()
    {
        Temperature temperature = Temperature.Of(312 - 273.15).Metric<Celsius>();
        Temperature expected = Temperature.Of(312).Si<Kelvin>();

        Temperature actual = temperature.To.Si<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void CelsiusToMilliKelvin()
    {
        Temperature temperature = Temperature.Of(-273.13534324).Metric<Celsius>();
        Temperature expected = Temperature.Of(14.65676).Si<Milli, Kelvin>();

        Temperature actual = temperature.To.Si<Milli, Kelvin>();

        actual.Matches(expected, VeryLowPrecision - 1);
    }
    [Fact]
    public void CelsiusToFahrenheit()
    {
        Temperature temperature = Temperature.Of(37.0).Metric<Celsius>();
        Temperature expected = Temperature.Of(98.6).Imperial<Fahrenheit>();

        Temperature actual = temperature.To.Imperial<Fahrenheit>();

        actual.Matches(expected);
    }
    [Fact]
    public void FahrenheitToKelvin()
    {
        Temperature temperature = Temperature.Of(-40).Imperial<Fahrenheit>();
        Temperature expected = Temperature.Of(233.15).Si<Kelvin>();

        Temperature actual = temperature.To.Si<Kelvin>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void CelsiusToGasMark()
    {
        Temperature temperature = Temperature.Of(218d + (1d / 3d)).Metric<Celsius>();
        Temperature expected = Temperature.Of(7).Imperial<GasMark>();

        Temperature actual = temperature.To.Imperial<GasMark>();

        actual.Matches(expected, MediumPrecision);
    }

    [Fact]
    public void FahrenheitToGasMark()
    {
        Temperature temperature = Temperature.Of(350).Imperial<Fahrenheit>();
        Temperature expected = Temperature.Of(4).Imperial<GasMark>();

        Temperature actual = temperature.To.Imperial<GasMark>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void GasMarkToFahrenheit()
    {
        Temperature temperature = Temperature.Of(1).Imperial<GasMark>();
        Temperature expected = Temperature.Of(275).Imperial<Fahrenheit>();

        Temperature actual = temperature.To.Imperial<Fahrenheit>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void GasMarkToKelvin()
    {
        Temperature temperature = Temperature.Of(1).Imperial<GasMark>();
        Temperature expected = Temperature.Of(408.15).Si<Kelvin>();

        Temperature actual = temperature.To.Si<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToGasMark()
    {
        Temperature temperature = Temperature.Of(475).Si<Kelvin>();
        Temperature expected = Temperature.Of(5.8132).Imperial<GasMark>();

        Temperature actual = temperature.To.Imperial<GasMark>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void KelvinToRankine()
    {
        Temperature temperature = Temperature.Of(255.37).Si<Kelvin>();
        Temperature expected = Temperature.Of(459.666).Imperial<Rankine>();

        Temperature actual = temperature.To.Imperial<Rankine>();

        actual.Matches(expected);
    }
    [Fact]
    public void RankineToKelvin()
    {
        Temperature temperature = Temperature.Of(671.64102).Imperial<Rankine>();
        Temperature expected = Temperature.Of(373.1339).Si<Kelvin>();

        Temperature actual = temperature.To.Si<Kelvin>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void FahrenheitToRankine()
    {
        Temperature temperature = Temperature.Of(32).Imperial<Fahrenheit>();
        Temperature expected = Temperature.Of(491.67).Imperial<Rankine>();

        Temperature actual = temperature.To.Imperial<Rankine>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void KelvinToDelisle()
    {
        Temperature temperature = Temperature.Of(273.15).Si<Kelvin>();
        Temperature expected = Temperature.Of(150).NonStandard<Delisle>();

        Temperature actual = temperature.To.NonStandard<Delisle>();

        actual.Matches(expected);
    }
    [Fact]
    public void DelisleToKelvin()
    {
        Temperature temperature = Temperature.Of(264).NonStandard<Delisle>();
        Temperature expected = Temperature.Of(197.15).Si<Kelvin>();

        Temperature actual = temperature.To.Si<Kelvin>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void KelvinToNewton()
    {
        Temperature temperature = Temperature.Of(373.15).Si<Kelvin>();
        Temperature expected = Temperature.Of(33).NonStandard<Newton>();

        Temperature actual = temperature.To.NonStandard<Newton>();

        actual.Matches(expected);
    }
    [Fact]
    public void NewtonToKelvin()
    {
        Temperature temperature = Temperature.Of(0).NonStandard<Newton>();
        Temperature expected = Temperature.Of(273.15).Si<Kelvin>();

        Temperature actual = temperature.To.Si<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToRéaumur()
    {
        Temperature temperature = Temperature.Of(1941).Si<Kelvin>();
        Temperature expected = Temperature.Of(1334.28).NonStandard<Réaumur>();

        Temperature actual = temperature.To.NonStandard<Réaumur>();

        actual.Matches(expected);
    }
    [Fact]
    public void RéaumurToKelvin()
    {
        Temperature temperature = Temperature.Of(-14.22).NonStandard<Réaumur>();
        Temperature expected = Temperature.Of(255.375).Si<Kelvin>();

        Temperature actual = temperature.To.Si<Kelvin>();

        actual.Matches(expected, MediumPrecision);
    }
    [Fact]
    public void KelvinToRømer()
    {
        Temperature temperature = Temperature.Of(255.37).Si<Kelvin>();
        Temperature expected = Temperature.Of(-1.8345).NonStandard<Rømer>();

        Temperature actual = temperature.To.NonStandard<Rømer>();

        actual.Matches(expected, VeryLowPrecision);
    }
    [Fact]
    public void RømerToKelvin()
    {
        Temperature temperature = Temperature.Of(7.50525).NonStandard<Rømer>();
        Temperature expected = Temperature.Of(273.16).Si<Kelvin>();

        Temperature actual = temperature.To.Si<Kelvin>();

        actual.Matches(expected, MediumPrecision);
    }
}
