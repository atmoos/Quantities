namespace Atmoos.Quantities.Physics;


public static class OhmsLaw
{
    extension(ElectricPotential)
    {
        public static ElectricCurrent operator /(in ElectricPotential electricPotential, in ElectricalResistance resistance) => ElectricCurrent.From(in electricPotential, in resistance);
        public static ElectricalResistance operator /(in ElectricPotential electricPotential, in ElectricCurrent current) => ElectricalResistance.From(in electricPotential, in current);
    }
    extension(ElectricCurrent)
    {
        public static ElectricPotential operator *(in ElectricCurrent current, in ElectricalResistance resistance) => ElectricPotential.From(in current, in resistance);
    }
    extension(ElectricalResistance)
    {
        public static ElectricPotential operator *(in ElectricalResistance resistance, in ElectricCurrent current) => ElectricPotential.From(in current, in resistance);
    }
}

public static class PowerLaws
{
    extension(ElectricPotential)
    {
        public static Power operator *(in ElectricPotential potential, in ElectricCurrent current) => Power.From(in potential, in current);
    }
    extension(ElectricCurrent)
    {
        public static Power operator *(in ElectricCurrent current, in ElectricPotential potential) => Power.From(in potential, in current);
    }
    extension(Power)
    {
        public static ElectricCurrent operator /(in Power power, in ElectricPotential potential) => ElectricCurrent.From(in power, in potential);
        public static ElectricPotential operator /(in Power power, in ElectricCurrent current) => ElectricPotential.From(in power, in current);
    }
}
