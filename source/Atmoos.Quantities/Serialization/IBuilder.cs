﻿namespace Atmoos.Quantities.Serialization;

internal interface IBuilder
{
    Quantity Build(in Double value);
}
