using static Atmoos.Quantities.TestTools.Assert;

using static Atmoos.Quantities.Systems;
using static Atmoos.Quantities.System.Si;

namespace Atmoos.Quantities.Test.System;

public class SiTest
{
    [Fact] public void Meter() => Condition(Length.Of(1, Si<Metre>()) == 1 * m);
    [Fact] public void KiloMeter() => Condition(Length.Of(1, Si<Kilo, Metre>()) == 1 * Km);
    [Fact] public void CentiMeter() => Condition(Length.Of(1, Si<Centi, Metre>()) == 1 * cm);
    [Fact] public void MilliMeter() => Condition(Length.Of(1, Si<Milli, Metre>()) == 1 * mm);
    [Fact] public void MicroMeter() => Condition(Length.Of(1, Si<Micro, Metre>()) == 1 * μm);
}
