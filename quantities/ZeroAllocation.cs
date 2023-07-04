namespace Quantities;

internal static class ZeroAllocation<TItem>
    where TItem : new()
{
    public static TItem Item { get; } = new();
}
