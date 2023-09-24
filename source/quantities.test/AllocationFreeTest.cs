namespace Quantities.Test;

public class AllocationFreeTest
{
    [Fact]
    public void AllocationFree_Creates_Singletons()
    {
        var instanceOne = AllocationFree<Object>.Item;
        var instanceTwo = AllocationFree<Object>.Item;

        Assert.Same(instanceOne, instanceTwo);
    }

    [Fact]
    public void AllocationFree_Is_ThreadAgnostic()
    {
        var a = ThreadedCreate("a");
        var b = ThreadedCreate("b");

        Assert.Same(a.obj, b.obj);
        Assert.NotEqual(a.threadId, b.threadId); // sanity check...

        static (Object obj, Int32 threadId) ThreadedCreate(String id)
        {
            Object? obj = null;
            var thread = new Thread(CreateObject);
            thread.Start();
            thread.Join();
            return (obj ?? $"No object created for '{id}'.", thread.ManagedThreadId);
            void CreateObject(Object? _) => obj = AllocationFree<Object>.Item;
        }
    }
}
