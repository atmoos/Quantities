using Quantities.Units.Imperial.Temperature;
using Quantities.Units.Other.Temperature;

namespace Quantities.Test;

// For a lot of the values here, see: https://en.wikipedia.org/wiki/Conversion_of_units_of_temperature#Comparison_of_temperature_scales
public sealed class TemperatureTest
{
    [Fact]
    public void KelvinToString() => FormattingMatches(v => Temperature.Si<Kelvin>(v), "K");
    [Fact]
    public void CelsiusToString() => FormattingMatches(v => Temperature.Celsius(v), "°C");
    [Fact]
    public void DelisleToString() => FormattingMatches(v => Temperature.Other<Delisle>(v), "°De");
    [Fact]
    public void NewtonToString() => FormattingMatches(v => Temperature.Other<Newton>(v), "°N");
    [Fact]
    public void RéaumurToString() => FormattingMatches(v => Temperature.Other<Réaumur>(v), "°Ré");
    [Fact]
    public void RømerToString() => FormattingMatches(v => Temperature.Other<Rømer>(v), "°Rø");
    [Fact]
    public void FahrenheitToString() => FormattingMatches(v => Temperature.Imperial<Fahrenheit>(v), "°F");
    [Fact]
    public void RankineToString() => FormattingMatches(v => Temperature.Imperial<Rankine>(v), "°R");
    [Fact]
    public void GasMarkToString() => FormattingMatches(v => Temperature.Imperial<GasMark>(v), "GM");
    [Fact]
    public void AddThreeTemperatures()
    {
        Temperature a = Temperature.Celsius(-40);
        Temperature b = Temperature.Imperial<Fahrenheit>(-40);
        Temperature c = Temperature.Si<Kelvin>(363.15);
        Temperature expected = Temperature.Celsius(10);

        Temperature actual = a + b + c;

        actual.Matches(expected);
    }
    [Fact]
    public void SubtractTemperatures()
    {
        Temperature a = Temperature.Celsius(70);
        Temperature b = Temperature.Si<Kelvin>(273.15 + 28);
        Temperature expected = Temperature.Celsius(42);

        Temperature actual = a - b;

        actual.Matches(expected);
    }

    [Fact]
    public void KelvinToCelsius()
    {
        Temperature temperature = Temperature.Si<Kelvin>(36 + 273.15);
        Temperature expected = Temperature.Celsius(36);

        Temperature actual = temperature.ToCelsius();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToFahrenheit()
    {
        Temperature temperature = Temperature.Si<Kelvin>(364);
        Temperature expected = Temperature.Imperial<Fahrenheit>(195.53);

        Temperature actual = temperature.ToImperial<Fahrenheit>();

        actual.Matches(expected);
    }
    [Fact]
    public void CelsiusToKelvin()
    {
        Temperature temperature = Temperature.Celsius(312 - 273.15);
        Temperature expected = Temperature.Si<Kelvin>(312);

        Temperature actual = temperature.To<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void CelsiusToMilliKelvin()
    {
        Temperature temperature = Temperature.Celsius(-273.13534324);
        Temperature expected = Temperature.Si<Milli, Kelvin>(14.65676);

        Temperature actual = temperature.To<Milli, Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void CelsiusToFahrenheit()
    {
        Temperature temperature = Temperature.Celsius(37.0);
        Temperature expected = Temperature.Imperial<Fahrenheit>(98.6);

        Temperature actual = temperature.ToImperial<Fahrenheit>();

        actual.Matches(expected);
    }
    [Fact]
    public void FahrenheitToKelvin()
    {
        Temperature temperature = Temperature.Imperial<Fahrenheit>(-40);
        Temperature expected = Temperature.Si<Kelvin>(233.15);

        Temperature actual = temperature.To<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void CelsiusToGasMark()
    {
        Temperature temperature = Temperature.Celsius(218d + (1d / 3d));
        Temperature expected = Temperature.Imperial<GasMark>(7);

        Temperature actual = temperature.ToImperial<GasMark>();

        actual.Matches(expected, VeryLowPrecision);
    }

    [Fact]
    public void FahrenheitToGasMark()
    {
        Temperature temperature = Temperature.Imperial<Fahrenheit>(350);
        Temperature expected = Temperature.Imperial<GasMark>(4);

        Temperature actual = temperature.ToImperial<GasMark>();

        actual.Matches(expected, VeryLowPrecision);
    }
    [Fact]
    public void GasMarkToFahrenheit()
    {
        Temperature temperature = Temperature.Imperial<GasMark>(1);
        Temperature expected = Temperature.Imperial<Fahrenheit>(275);

        Temperature actual = temperature.ToImperial<Fahrenheit>();

        actual.Matches(expected);
    }
    [Fact]
    public void GasMarkToKelvin()
    {
        Temperature temperature = Temperature.Imperial<GasMark>(1);
        Temperature expected = Temperature.Si<Kelvin>(408.15);

        Temperature actual = temperature.To<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToGasMark()
    {
        Temperature temperature = Temperature.Si<Kelvin>(475);
        Temperature expected = Temperature.Imperial<GasMark>(5.8132);

        Temperature actual = temperature.ToImperial<GasMark>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToRankine()
    {
        Temperature temperature = Temperature.Si<Kelvin>(255.37);
        Temperature expected = Temperature.Imperial<Rankine>(459.666);

        Temperature actual = temperature.ToImperial<Rankine>();

        actual.Matches(expected);
    }
    [Fact]
    public void RankineToKelvin()
    {
        Temperature temperature = Temperature.Imperial<Rankine>(671.64102);
        Temperature expected = Temperature.Si<Kelvin>(373.1339);

        Temperature actual = temperature.To<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void FahrenheitToRankine()
    {
        Temperature temperature = Temperature.Imperial<Fahrenheit>(32);
        Temperature expected = Temperature.Imperial<Rankine>(491.67);

        Temperature actual = temperature.ToImperial<Rankine>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToDelisle()
    {
        Temperature temperature = Temperature.Si<Kelvin>(273.16);
        Temperature expected = Temperature.Other<Delisle>(149.985);

        Temperature actual = temperature.ToOther<Delisle>();

        actual.Matches(expected);
    }
    [Fact]
    public void DelisleToKelvin()
    {
        Temperature temperature = Temperature.Other<Delisle>(176.67);
        Temperature expected = Temperature.Si<Kelvin>(255.37);

        Temperature actual = temperature.To<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToNewton()
    {
        Temperature temperature = Temperature.Si<Kelvin>(373.15);
        Temperature expected = Temperature.Other<Newton>(33);

        Temperature actual = temperature.ToOther<Newton>();

        actual.Matches(expected);
    }
    [Fact]
    public void NewtonToKelvin()
    {
        Temperature temperature = Temperature.Other<Newton>(0);
        Temperature expected = Temperature.Si<Kelvin>(273.15);

        Temperature actual = temperature.To<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToRéaumur()
    {
        Temperature temperature = Temperature.Si<Kelvin>(1941);
        Temperature expected = Temperature.Other<Réaumur>(1334.28);

        Temperature actual = temperature.ToOther<Réaumur>();

        actual.Matches(expected);
    }
    [Fact]
    public void RéaumurToKelvin()
    {
        Temperature temperature = Temperature.Other<Réaumur>(-14.22);
        Temperature expected = Temperature.Si<Kelvin>(255.375);

        Temperature actual = temperature.To<Kelvin>();

        actual.Matches(expected);
    }
    [Fact]
    public void KelvinToRømer()
    {
        Temperature temperature = Temperature.Si<Kelvin>(255.37);
        Temperature expected = Temperature.Other<Rømer>(-1.8345);

        Temperature actual = temperature.ToOther<Rømer>();

        actual.Matches(expected);
    }
    [Fact]
    public void RømerToKelvin()
    {
        Temperature temperature = Temperature.Other<Rømer>(7.50525);
        Temperature expected = Temperature.Si<Kelvin>(273.16);

        Temperature actual = temperature.To<Kelvin>();

        actual.Matches(expected);
    }
}
