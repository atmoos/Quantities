using static Xunit.Assert;

using static Atmoos.Quantities.Systems;
using static Atmoos.Quantities.System.Imperial;

namespace Atmoos.Quantities.Test.System;

public class ImperialTest
{
    [Fact] public void Foot() => Equal(Length.Of(1, Imperial<Foot>()), 1 * ft);
    [Fact] public void Mile() => Equal(Length.Of(1, Imperial<Mile>()), 1 * mi);
    [Fact] public void Inch() => Equal(Length.Of(1, Imperial<Inch>()), 1 * @in);
}
