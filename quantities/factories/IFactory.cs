namespace Quantities.Factories;

public interface IFactory { /* marker interface */ }

public interface IFactory<out TTo, out TCreate> : IFactory
    where TTo : IFactory
    where TCreate : IFactory
{
    public TTo To { get; }
    public static abstract TCreate Of(in Double value);
}
public interface IFactory<in TFactory, out TTo, out TCreate> : IFactory<TTo, TCreate>
    where TFactory : IFactory
    where TTo : TFactory
    where TCreate : TFactory
{
}