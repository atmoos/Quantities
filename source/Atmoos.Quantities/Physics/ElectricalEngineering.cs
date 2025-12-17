using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Physics;

public static class OhmsLaw
{
    extension(ElectricPotential)
    {
        public static ElectricCurrent operator /(in ElectricPotential electricPotential, in ElectricalResistance resistance) => Create<ElectricCurrent>(electricPotential.Value / resistance.Value);
        public static ElectricalResistance operator /(in ElectricPotential electricPotential, in ElectricCurrent current) => Create<ElectricalResistance>(electricPotential.Value / current.Value);
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
