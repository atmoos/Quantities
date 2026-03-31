using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Physics;

public static class OhmsLaw
{
    extension(ElectricPotential)
    {
        public static ElectricCurrent operator /(in ElectricPotential electricPotential, in ElectricalResistance resistance) =>
            Create<ElectricCurrent>(electricPotential.Value / resistance.Value);

        public static ElectricalResistance operator /(in ElectricPotential electricPotential, in ElectricCurrent current) =>
            Create<ElectricalResistance>(electricPotential.Value / current.Value);
    }

    extension(ElectricCurrent)
    {
        public static ElectricPotential operator *(in ElectricCurrent current, in ElectricalResistance resistance) => Create<ElectricPotential>(current.Value * resistance.Value);
    }

    extension(ElectricalResistance)
    {
        public static ElectricPotential operator *(in ElectricalResistance resistance, in ElectricCurrent current) => Create<ElectricPotential>(current.Value * resistance.Value);
    }
}

public static class PowerLaws
{
    extension(ElectricPotential)
    {
        public static Power operator *(in ElectricPotential potential, in ElectricCurrent current) => Create<Power>(potential.Value * current.Value);
    }

    extension(ElectricCurrent)
    {
        public static Power operator *(in ElectricCurrent current, in ElectricPotential potential) => Create<Power>(potential.Value * current.Value);
    }

    extension(Power)
    {
        public static ElectricCurrent operator /(in Power power, in ElectricPotential potential) => Create<ElectricCurrent>(power.Value / potential.Value);

        public static ElectricPotential operator /(in Power power, in ElectricCurrent current) => Create<ElectricPotential>(power.Value / current.Value);
    }
}

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public static class ChargeLaws
{
    extension(ElectricCurrent)
    {
        public static ElectricCharge operator *(in ElectricCurrent current, in Time time) => Create<ElectricCharge>(current.Value * time.Value);
    }

    extension(Time)
    {
        public static ElectricCharge operator *(in Time time, in ElectricCurrent current) => Create<ElectricCharge>(time.Value * current.Value);
    }

    extension(ElectricCharge)
    {
        public static ElectricCurrent operator /(in ElectricCharge charge, in Time time) => Create<ElectricCurrent>(charge.Value / time.Value);

        public static Time operator /(in ElectricCharge charge, in ElectricCurrent current) => Create<Time>(charge.Value / current.Value);
    }
}
