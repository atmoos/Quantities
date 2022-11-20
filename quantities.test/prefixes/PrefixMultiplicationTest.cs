using Quantities.Measures;
using Quantities.Unit.Si.Derived;

namespace Quantities.Test.Prefixes;

public class PrefixMultiplicationTest
{
    [Fact]
    public void MultiplicationWorks()
    {
        Quant volts = 120d.As<SiDerived<Milli, Volt>>();
        Quant ampere = 3d.As<Si<Micro, Ampere>>();
        Quant expected = 40d.As<SiDerived<Kilo, Ohm>>();

        Double siOhm = volts / ampere;
        Quant actual = SiPrefix.Scale(siOhm, new MakeOhm());

        Assert.Equal(expected, actual);
    }

    private sealed class MakeOhm : IPrefixInject<Quant>
    {
        public Quant Identity(in Double value)
        {
            return value.As<SiDerived<Ohm>>();
        }

        public Quant Inject<TPrefix>(in Double value) where TPrefix : IPrefix
        {
            return value.As<SiDerived<TPrefix, Ohm>>();
        }
    }
}
