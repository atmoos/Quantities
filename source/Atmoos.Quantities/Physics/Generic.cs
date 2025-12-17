using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Physics;

public static class Generic
{
    extension(Double)
    {
        public static Frequency operator /(Double scalar, in Time time) => Create<Frequency>(scalar / time.Value);
        public static Time operator /(Double scalar, in Frequency frequency) => Create<Time>(scalar / frequency.Value);
    }

    extension(Time)
    {
        public static Double operator *(in Time time, in Frequency frequency) => time.Value * frequency.Value;
    }

    extension(Frequency)
    {
        public static Double operator *(in Frequency frequency, in Time time) => frequency.Value * time.Value;
    }
}
