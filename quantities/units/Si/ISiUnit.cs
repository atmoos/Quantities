namespace Quantities.Unit.Si;

public interface ISiUnit : IUnit
{
    internal static abstract Int32 Offset { get; }
}
