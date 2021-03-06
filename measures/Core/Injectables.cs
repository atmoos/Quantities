using System;
using Quantities.Unit;
using Quantities.Prefixes;
using Quantities.Dimensions;

namespace Quantities.Measures.Core
{
    internal interface IPrefixInjectable
    {
        void Inject<TPrefix>() where TPrefix : Prefix, new();
    }
    internal interface ISiInjectable<in TDimension>
        where TDimension : IDimension
    {
        void Inject<TInjectedDimension>(in Double value)
            where TInjectedDimension : SiMeasure, TDimension, new();
    }
    internal interface INonSiInjectable<in TDimension>
        where TDimension : IDimension
    {
        void Inject<TUnit>(in Double value) where TUnit : TDimension, IUnit, ITransform, new();
    }
    internal interface IInjectable<in TDimension> : ISiInjectable<TDimension>, INonSiInjectable<TDimension>
        where TDimension : IDimension
    { }
}