namespace Atmoos.Quantities.Physics.Mechanics;

public static class Geometry
{
    extension(Length)
    {
        public static Area operator *(in Length left, in Length right) => Area.From(in left, in right);
        public static Volume operator *(in Length length, in Area area) => Volume.Times(length, area);
    }
    extension(Area)
    {
        public static Volume operator *(in Area area, in Length length) => Volume.Times(in area, in length);
        public static Length operator /(in Area left, in Length right) => Length.From(in left, in right);
    }
    extension(Volume)
    {
        public static Length operator /(in Volume volume, in Area area) => Length.From(in volume, in area);
        public static Area operator /(in Volume volume, in Length length) => Area.From(in volume, in length);
    }
}

