namespace Atmoos.Quantities.Prefixes;

internal interface IPrefixInject<out T>
{
    T Identity(in Double value);
    T Inject<TPrefix>(in Double value)
        where TPrefix : IPrefix;
}
