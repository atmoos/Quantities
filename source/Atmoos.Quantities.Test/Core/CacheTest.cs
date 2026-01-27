namespace Atmoos.Quantities.Test.Core;

public sealed class CacheTest
{
    [Fact]
    public void ScalarCacheCreateFunctionIsCalledWhenTheCacheIsEmpty()
    {
        const Int32 key = 1;
        var createCalled = false;
        var cache = new Cache<Int32, String>();

        var value = cache[key, Add];

        Assert.True(createCalled);
        Assert.Equal(key.ToString(), value);

        String Add(Int32 key)
        {
            createCalled = true;
            return key.ToString();
        }
    }

    [Fact]
    public void ScalarCacheCreateFunctionIsNotCalledWhenTheCacheIsAlreadyPopulated()
    {
        const Int32 key = 21;
        var cache = new Cache<Int32, String>();
        var cachedValue = cache[key, key => key.ToString()];

        var actual = cache[key, _ => "This should not be called"];

        Assert.Equal(cachedValue, actual);
    }

    [Fact]
    public void BiCacheCreateFunctionIsCalledWhenTheCacheIsEmpty()
    {
        const Int32 leftKey = 1;
        const Int32 rightKey = 3;
        var createCalled = false;
        var cache = new Cache<Int32, Int32, String>();

        var value = cache[leftKey, rightKey, Add];

        Assert.True(createCalled);
        Assert.Equal($"{leftKey}+{rightKey}", value);

        String Add(Int32 lk, Int32 rk)
        {
            createCalled = true;
            return $"{lk}+{rk}";
        }
    }

    [Fact]
    public void BiCacheIsNotAssociative()
    {
        const Int32 leftKey = 23;
        const Int32 rightKey = 32;
        var cache = new Cache<Int32, Int32, Object>();

        var leftRight = cache[leftKey, rightKey, (_, _) => new Object()];
        var rightLeft = cache[rightKey, leftKey, (_, _) => new Object()];

        Assert.NotEqual(leftRight, rightLeft);
    }
}
