namespace Quantities.Factories;

public interface IFactory { /* marker interface */ }

public interface IFactory<out TFactory> : IFactory
    where TFactory : IFactory
{
    public static abstract TFactory Of(in Double value);
}

