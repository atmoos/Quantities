using Atmoos.Quantities.Common;

namespace Atmoos.Quantities.Test.Common;

[Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
public sealed class IntrospectionTest
{
    [Fact]
    public void MostDerivedOfReturnsDeepestInterface()
    {
        Type actual = typeof(MultiLevelImplementation).MostDerivedOf(typeof(ITopLevel));

        Assert.Equal(typeof(IDeepest), actual);
    }

    [Fact]
    public void MostDerivedOfGenericOverloadReturnsDeepestInterface()
    {
        Type actual = typeof(MultiLevelImplementation).MostDerivedOf<ITopLevel>();

        Assert.Equal(typeof(IDeepest), actual);
    }

    [Fact]
    public void InnerTypeReturnsOnlyGenericArgument()
    {
        Type actual = typeof(SingleGenericImplementation).InnerType(typeof(ISingleGeneric<>));

        Assert.Equal(typeof(Int32), actual);
    }

    [Fact]
    public void InnerTypeThrowsWhenNoGenericMatchExists()
    {
        Assert.Throws<InvalidOperationException>(() => typeof(NoGenericImplementation).InnerType(typeof(ISingleGeneric<>)));
    }

    [Fact]
    public void InnerTypeThrowsWhenMultipleGenericMatchesExist()
    {
        Assert.Throws<InvalidOperationException>(() => typeof(MultiGenericImplementation).InnerType(typeof(ISingleGeneric<>)));
    }

    [Fact]
    public void InnerTypesReturnsArgumentsForInterfaceInputs()
    {
        Type[] actual = typeof(ISingleGeneric<Int32>).InnerTypes(typeof(ISingleGeneric<>));

        Assert.Equal([typeof(Int32)], actual);
    }

    [Fact]
    public void InnerTypesReturnsMultipleArgumentsForInterfaceInputs()
    {
        Type[] actual = typeof(MultiGenericImplementation).InnerTypes(typeof(ISingleGeneric<>));

        Assert.Equal([typeof(Int32), typeof(Double)], actual);
    }

    [Fact]
    public void ImplementsAndImplementsGenericReturnExpectedValues()
    {
        Assert.True(typeof(MultiLevelImplementation).Implements(typeof(ITopLevel)));
        Assert.False(typeof(MultiLevelImplementation).Implements(typeof(IDisposable)));
        Assert.True(typeof(ISingleGeneric<Int32>).ImplementsGeneric(typeof(ISingleGeneric<>)));
        Assert.False(typeof(Int32).ImplementsGeneric(typeof(ISingleGeneric<>)));
    }

    private interface ITopLevel;

    private interface IMiddle : ITopLevel;

    private interface IDeepest : IMiddle;

    private interface IAlternative : ITopLevel;

    private sealed class MultiLevelImplementation : IDeepest, IAlternative;

    private interface ISingleGeneric<T>;

    private sealed class SingleGenericImplementation : ISingleGeneric<Int32>;

    private sealed class NoGenericImplementation;

    private sealed class MultiGenericImplementation : ISingleGeneric<Int32>, ISingleGeneric<Double>;
}
