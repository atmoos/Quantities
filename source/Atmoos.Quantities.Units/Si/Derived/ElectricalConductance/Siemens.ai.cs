using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Units.Si.Derived;

namespace Atmoos.Quantities.Units.Si.Derived.ElectricalConductance;

// See: https://en.wikipedia.org/wiki/Siemens_(unit)
[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public readonly struct Siemens : ISiUnit, IElectricalConductance, IInvertible<IElectricalResistance>
{
    static T ISystemInject<IElectricalResistance>.Inject<T>(ISystems<IElectricalResistance, T> basis) => basis.Si<Ohm>();

    public static String Representation => "S";
}
