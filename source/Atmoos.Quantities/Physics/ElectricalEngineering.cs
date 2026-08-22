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

        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static ElectricalConductance operator /(in ElectricCurrent current, in ElectricPotential electricPotential) => Create<ElectricalConductance>(current.Value / electricPotential.Value);
    }

    extension(ElectricalResistance)
    {
        public static ElectricPotential operator *(in ElectricalResistance resistance, in ElectricCurrent current) => Create<ElectricPotential>(current.Value * resistance.Value);
    }

    extension(ElectricPotential)
    {
        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static ElectricCurrent operator *(in ElectricPotential potential, in ElectricalConductance conductance) => Create<ElectricCurrent>(potential.Value * conductance.Value);
    }

    extension(ElectricalConductance)
    {
        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static ElectricCurrent operator *(in ElectricalConductance conductance, in ElectricPotential potential) => Create<ElectricCurrent>(conductance.Value * potential.Value);

        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static ElectricPotential operator /(in ElectricCurrent current, in ElectricalConductance conductance) => Create<ElectricPotential>(current.Value / conductance.Value);
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

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public static class CapacitanceLaws
{
    extension(ElectricCharge)
    {
        public static Capacitance operator /(in ElectricCharge charge, in ElectricPotential potential) => Create<Capacitance>(charge.Value / potential.Value);
    }

    extension(ElectricPotential)
    {
        public static ElectricCharge operator *(in ElectricPotential potential, in Capacitance capacitance) => Create<ElectricCharge>(potential.Value * capacitance.Value);
    }

    extension(Capacitance)
    {
        public static ElectricCharge operator *(in Capacitance capacitance, in ElectricPotential potential) => Create<ElectricCharge>(capacitance.Value * potential.Value);

        public static ElectricPotential operator /(in ElectricCharge charge, in Capacitance capacitance) => Create<ElectricPotential>(charge.Value / capacitance.Value);
    }
}

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public static class MagneticFluxLaws
{
    extension(ElectricPotential)
    {
        public static MagneticFlux operator *(in ElectricPotential potential, in Time time) => Create<MagneticFlux>(potential.Value * time.Value);
    }

    extension(Time)
    {
        public static MagneticFlux operator *(in Time time, in ElectricPotential potential) => Create<MagneticFlux>(time.Value * potential.Value);
    }

    extension(MagneticFlux)
    {
        public static ElectricPotential operator /(in MagneticFlux magneticFlux, in Time time) => Create<ElectricPotential>(magneticFlux.Value / time.Value);

        public static Time operator /(in MagneticFlux magneticFlux, in ElectricPotential potential) => Create<Time>(magneticFlux.Value / potential.Value);
    }
}

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public static class MagneticFluxDensityLaws
{
    extension(MagneticFlux)
    {
        public static MagneticFluxDensity operator /(in MagneticFlux magneticFlux, in Area area) => Create<MagneticFluxDensity>(magneticFlux.Value / area.Value);
    }

    extension(MagneticFluxDensity)
    {
        public static MagneticFlux operator *(in MagneticFluxDensity density, in Area area) => Create<MagneticFlux>(density.Value * area.Value);

        public static Area operator /(in MagneticFlux magneticFlux, in MagneticFluxDensity density) => Create<Area>(magneticFlux.Value / density.Value);
    }
}
