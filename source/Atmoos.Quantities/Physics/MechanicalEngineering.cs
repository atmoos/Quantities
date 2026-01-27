using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Physics;

public static class Geometry
{
    extension(Length)
    {
        public static Area operator *(in Length left, in Length right) => Create<Area>(left.Value * right.Value);

        public static Volume operator *(in Length length, in Area area) => Create<Volume>(length.Value * area.Value);
    }

    extension(Area)
    {
        public static Volume operator *(in Area area, in Length length) => Create<Volume>(area.Value * length.Value);

        public static Length operator /(in Area left, in Length right) => Create<Length>(left.Value / right.Value);
    }

    extension(Volume)
    {
        public static Length operator /(in Volume volume, in Area area) => Create<Length>(volume.Value / area.Value);

        public static Area operator /(in Volume volume, in Length length) => Create<Area>(volume.Value / length.Value);
    }
}

public static class Kinematics
{
    extension(Length)
    {
        public static Time operator /(in Length length, in Velocity velocity) => Create<Time>(length.Value / velocity.Value);

        public static Velocity operator /(in Length length, in Time time) => Create<Velocity>(length.Value / time.Value);
    }

    extension(Velocity)
    {
        public static Length operator *(in Velocity velocity, in Time time) => Create<Length>(velocity.Value * time.Value);

        public static Time operator /(in Velocity velocity, in Acceleration acceleration) => Create<Time>(velocity.Value / acceleration.Value);

        public static Acceleration operator /(in Velocity velocity, in Time time) => Create<Acceleration>(velocity.Value / time.Value);

        public static Power operator *(in Velocity velocity, in Force force) => Create<Power>(force.Value * velocity.Value);
    }

    extension(Acceleration)
    {
        public static Velocity operator *(in Acceleration acceleration, in Time time) => Create<Velocity>(acceleration.Value * time.Value);
    }

    extension(Area)
    {
        public static Force operator *(in Area area, in Pressure pressure) => Create<Force>(pressure.Value * area.Value);
    }

    extension(Time)
    {
        public static Energy operator *(in Time time, in Power power) => Create<Energy>(power.Value * time.Value);

        public static Length operator *(in Time time, in Velocity velocity) => Create<Length>(velocity.Value * time.Value);

        public static Velocity operator *(in Time time, in Acceleration acceleration) => Create<Velocity>(acceleration.Value * time.Value);
    }

    extension(Pressure)
    {
        public static Force operator *(in Pressure pressure, in Area area) => Create<Force>(pressure.Value * area.Value);
    }

    extension(Force)
    {
        public static Pressure operator /(in Force force, in Area area) => Create<Pressure>(force.Value / area.Value);

        public static Power operator *(in Force force, in Velocity velocity) => Create<Power>(force.Value * velocity.Value);
    }

    extension(Power)
    {
        public static Velocity operator /(in Power power, in Force force) => Create<Velocity>(power.Value / force.Value);

        public static Force operator /(in Power power, in Velocity velocity) => Create<Force>(power.Value / velocity.Value);

        public static Energy operator *(in Power power, in Time time) => Create<Energy>(power.Value * time.Value);
    }

    extension(Energy)
    {
        public static Power operator /(in Energy energy, in Time time) => Create<Power>(energy.Value / time.Value);

        public static Time operator /(in Energy energy, in Power power) => Create<Time>(energy.Value / power.Value);
    }
}
