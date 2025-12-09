namespace Atmoos.Quantities.Physics.Mechanics;

public static class Kinematics
{
    extension(Length)
    {
        public static Time operator /(in Length length, in Velocity velocity) => Time.From(in length, in velocity);
        public static Velocity operator /(in Length length, in Time time) => Velocity.From(in length, in time);
    }
    extension(Velocity)
    {
        public static Length operator *(in Velocity velocity, in Time time) => Length.From(in velocity, in time);
        public static Time operator /(in Velocity velocity, in Acceleration acceleration) => Time.From(in velocity, in acceleration);
        public static Acceleration operator /(in Velocity velocity, in Time time) => Acceleration.From(in velocity, in time);
        public static Power operator *(in Velocity velocity, in Force force) => Power.From(in force, in velocity);
    }
    extension(Acceleration)
    {
        public static Velocity operator *(in Acceleration acceleration, in Time time) => Velocity.From(in acceleration, in time);
    }
    extension(Area)
    {
        public static Force operator *(in Area area, in Pressure pressure) => Force.From(in pressure, in area);
    }
    extension(Time)
    {
        public static Energy operator *(in Time time, in Power power) => Energy.From(in power, in time);
        public static Length operator *(in Time time, in Velocity velocity) => Length.From(in velocity, in time);
        public static Velocity operator *(in Time time, in Acceleration acceleration) => Velocity.From(in acceleration, in time);
    }
    extension(Pressure)
    {
        public static Force operator *(in Pressure pressure, in Area area) => Force.From(in pressure, in area);
    }
    extension(Force)
    {
        public static Pressure operator /(in Force force, in Area area) => Pressure.From(in force, in area);
        public static Power operator *(in Force force, in Velocity velocity) => Power.From(in force, in velocity);
    }
    extension(Power)
    {
        public static Velocity operator /(in Power power, in Force force) => Velocity.From(in power, in force);
        public static Force operator /(in Power power, in Velocity velocity) => Force.From(in power, in velocity);
        public static Energy operator *(in Power power, in Time time) => Energy.From(in power, in time);
    }
    extension(Energy)
    {
        public static Power operator /(in Energy energy, in Time time) => Power.From(in energy, in time);
        public static Time operator /(in Energy energy, in Power power) => Time.From(in energy, in power);
    }
}
