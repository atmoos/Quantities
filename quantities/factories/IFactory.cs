namespace Quantities.Factories;

public interface IFactory { /* marker interface */ }

public interface IFactory<in TFactory, out TTo, out TCreate> : IFactory
    where TFactory : IFactory
    where TTo : TFactory
    where TCreate : TFactory
{
    public TTo To { get; }
    public static abstract TCreate Of(in Double value);
}