﻿namespace Quantities.Serialization;

internal interface IBuilder
{
    Quantity Build(in Double value);
}

internal interface IInject
{
    IBuilder Inject<TMeasure>() where TMeasure : IMeasure;
}
