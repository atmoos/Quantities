namespace Quantities.Core;

internal static class AllocationFree<TItem>
    where TItem : class, new()
{
    public static TItem Item { get; } = new();
}
