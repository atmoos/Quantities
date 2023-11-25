namespace Quantities.Core;

internal interface IInject<out TResult>
{
    TResult Inject<TMeasure>() where TMeasure : IMeasure;
}
