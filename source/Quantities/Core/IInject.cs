namespace Quantities.Core;

internal interface IInject<out TResult>
{
    TResult Inject<TMeasure>() where TMeasure : IMeasure;
}

internal interface IInjector
{
    TResult Inject<TResult>(IInject<TResult> inject);
}
