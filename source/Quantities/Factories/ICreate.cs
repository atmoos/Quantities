namespace Quantities.Factories;

public interface ICreate
{
    internal Quantity Create<TMeasure>()
        where TMeasure : IMeasure;
}
