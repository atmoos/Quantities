using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class VolumetricFlowRateTest
{
    [Fact]
    public void CubicMetrePerSecondToString() => FormattingMatches(v => VolumetricFlowRate.Of(v, Cubic(Si<Metre>()).Per(Si<Second>())), "m³/s");

    [Fact]
    public void LambdaPerSecondToString() => FormattingMatches(v => VolumetricFlowRate.Of(v, Metric<Lambda>().Per(Si<Second>())), "λ/s");

    [Fact]
    public void LambdaPerSecondToCubicMetrePerSecond()
    {
        VolumetricFlowRate flowRate = VolumetricFlowRate.Of(1000000000, Metric<Lambda>().Per(Si<Second>()));
        VolumetricFlowRate expected = VolumetricFlowRate.Of(1, Cubic(Si<Metre>()).Per(Si<Second>()));

        VolumetricFlowRate actual = flowRate.To(Cubic(Si<Metre>()).Per(Si<Second>()));

        actual.Matches(expected);
    }

    [Fact]
    public void CubicMetrePerSecondToLambdaPerSecond()
    {
        VolumetricFlowRate flowRate = VolumetricFlowRate.Of(1, Cubic(Si<Metre>()).Per(Si<Second>()));
        VolumetricFlowRate expected = VolumetricFlowRate.Of(1000000000, Metric<Lambda>().Per(Si<Second>()));

        VolumetricFlowRate actual = flowRate.To(Metric<Lambda>().Per(Si<Second>()));

        actual.Matches(expected);
    }
}
