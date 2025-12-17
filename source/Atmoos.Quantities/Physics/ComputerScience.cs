using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Physics;

public static class ComputerScience
{
    extension(Data)
    {
        public static DataRate operator /(in Data data, in Time time) => Create<DataRate>(data.Value / time.Value);
    }
    extension(DataRate)
    {
        public static Data operator *(in DataRate rate, in Time time) => Create<Data>(rate.Value * time.Value);
    }
    extension(Time)
    {
        public static Data operator *(in Time time, in DataRate rate) => Create<Data>(time.Value * rate.Value);
    }
}
