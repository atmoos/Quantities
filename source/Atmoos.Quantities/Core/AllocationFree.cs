namespace Atmoos.Quantities.Core;

internal static class AllocationFree<TItem>
    where TItem : class, new()
{
    private static readonly TItem item = new();
    public static ref readonly TItem Item => ref item;
}

internal static class AllocationFree<TReference, TItem>
    where TReference : class
    where TItem : class, TReference, new()
{
    private static readonly TReference item = new TItem();
    public static ref readonly TReference Item => ref item;
}
