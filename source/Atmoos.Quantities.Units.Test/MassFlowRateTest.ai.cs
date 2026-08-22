using Atmoos.Quantities.Units.Si.Metric;

namespace Atmoos.Quantities.Units.Test;

[Ai(Model = "GPT", Version = "5.4", Variant = "Copilot")]
public sealed class MassFlowRateTest
{
    [Fact]
    public void KilogramPerSecondToString() => FormattingMatches(v => MassFlowRate.Of(v, Si<Kilogram>().Per(Si<Second>())), "kg/s");

    [Fact]
    public void GramPerSecondToString() => FormattingMatches(v => MassFlowRate.Of(v, Metric<Gram>().Per(Si<Second>())), "g/s");

    [Fact]
    public void GramPerSecondToKilogramPerSecond()
    {
        MassFlowRate flowRate = MassFlowRate.Of(1000, Metric<Gram>().Per(Si<Second>()));
        MassFlowRate expected = MassFlowRate.Of(1, Si<Kilogram>().Per(Si<Second>()));

        MassFlowRate actual = flowRate.To(Si<Kilogram>().Per(Si<Second>()));

        actual.Matches(expected);
    }

    [Fact]
    public void KilogramPerSecondToGramPerSecond()
    {
        MassFlowRate flowRate = MassFlowRate.Of(1, Si<Kilogram>().Per(Si<Second>()));
        MassFlowRate expected = MassFlowRate.Of(1000, Metric<Gram>().Per(Si<Second>()));

        MassFlowRate actual = flowRate.To(Metric<Gram>().Per(Si<Second>()));

        actual.Matches(expected);
    }
}
