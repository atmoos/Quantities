namespace Atmoos.Quantities.Physics.ComputerScience;

public static class Information
{
    extension(Data)
    {
        public static DataRate operator /(in Data data, in Time time) => DataRate.From(in data, in time);
    }
    extension(DataRate)
    {
        public static Data operator *(in DataRate rate, in Time time) => Data.From(in rate, in time);
    }
    extension(Time)
    {
        public static Data operator *(in Time time, in DataRate rate) => Data.From(in time, in rate);
    }
}
