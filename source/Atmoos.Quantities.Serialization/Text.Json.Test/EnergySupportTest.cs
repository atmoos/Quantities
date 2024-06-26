﻿namespace Atmoos.Quantities.Serialization.Text.Json.Test;

public class EnergySupportTest : ISerializationTester<Energy>
{
    [Theory]
    [MemberData(nameof(Quantities))]
    public void SupportsSerialization(Energy quantity) => quantity.SupportsSerialization();
    public static IEnumerable<Object[]> Quantities()
    {
        static IEnumerable<Energy> Interesting()
        {
            yield return Energy.Of(21, Si<Kilo, Joule>());
            yield return Energy.Of(21, Si<Watt>().Times(Metric<Hour>()));
            yield return Energy.Of(342, Si<Milli, Watt>().Times(Metric<Minute>()));
            yield return Energy.Of(342, Si<Kilo, Watt>().Times(Metric<Hour>()));
            yield return Energy.Of(6, Si<Mega, Watt>().Times(Si<Second>()));
        }
        return Interesting().Select(l => new Object[] { l });
    }
}
