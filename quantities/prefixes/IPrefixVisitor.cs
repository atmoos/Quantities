namespace Quantities.Prefixes;

public interface IPrefixVisitor<T>
{
    (IPrefixVisitor<T> next, T value) Visit<TPrefix>(in T value) where TPrefix : IPrefix;
}
