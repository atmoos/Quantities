using static Xunit.Assert;

using static Atmoos.Quantities.Systems;
using static Atmoos.Quantities.System.Si;

namespace Atmoos.Quantities.Test.System;

public class SiTest
{
    [Fact] public void Meter() => Equal(Length.Of(1, Si<Metre>()), 1 * m);
    [Fact] public void KiloMeter() => Equal(Length.Of(1, Si<Kilo, Metre>()), 1 * Km);
    [Fact] public void CentiMeter() => Equal(Length.Of(1, Si<Centi, Metre>()), 1 * cm);
    [Fact] public void MilliMeter() => Equal(Length.Of(1, Si<Milli, Metre>()), 1 * mm);
    [Fact] public void MicroMeter() => Equal(Length.Of(1, Si<Micro, Metre>()), 1 * μm);
}
