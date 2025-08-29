namespace Atmoos.Quantities.Core;

internal delegate TValue Create<in TKey, out TValue>(TKey key);
internal delegate TValue CreateArg<in TKey, TArg, out TValue>(TKey key, TArg arg);
internal delegate TValue Create<in TKeyOne, in TKeyTwo, out TValue>(TKeyOne firstKey, TKeyTwo secondKey);
internal delegate TValue CreateArg<in TKeyOne, in TKeyTwo, TArg, out TValue>(TKeyOne firstKey, TKeyTwo secondKey, TArg arg);
internal sealed class Cache<TKey, TValue>
    where TKey : notnull
    where TValue : notnull
{
    private readonly Dictionary<TKey, TValue> cache = [];

    public TValue this[in TKey key, Create<TKey, TValue> create]
        => this.cache.TryGetValue(key, out var value) ? value : this.cache[key] = create(key);
    public TValue Get<TArg>(in TKey key, in TArg arg, CreateArg<TKey, TArg, TValue> create)
    {
        return this.cache.TryGetValue(key, out var value) ? value : this.cache[key] = create(key, arg);
    }
}

internal sealed class Cache<TKeyOne, TKeyTwo, TValue>
    where TKeyOne : notnull
    where TKeyTwo : notnull
    where TValue : notnull
{
    private readonly Dictionary<TKeyOne, Dictionary<TKeyTwo, TValue>> cache = [];

    public TValue this[TKeyOne firstKey, TKeyTwo secondKey, Create<TKeyOne, TKeyTwo, TValue> create]
        => this.Get(firstKey, secondKey, create, (f, s, c) => c(f, s));

    public TValue Get<TArg>(TKeyOne firstKey, TKeyTwo secondKey, in TArg arg, CreateArg<TKeyOne, TKeyTwo, TArg, TValue> create)
    {
        if (this.cache.TryGetValue(firstKey, out var secondCache)) {
            if (secondCache.TryGetValue(secondKey, out var cachedValue)) {
                return cachedValue;
            }
            return secondCache[secondKey] = create(firstKey, secondKey, arg);
        }
        var value = create(firstKey, secondKey, arg);
        this.cache[firstKey] = new Dictionary<TKeyTwo, TValue> { [secondKey] = value };
        return value;
    }
}
