namespace Quantities.Measures;

internal interface ICreate<out T>
{
    T Create<TMeasure>(in Double value) where TMeasure : IMeasure;
}