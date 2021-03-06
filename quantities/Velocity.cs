using System;
using Quantities.Unit.Si;
using Quantities.Unit.Imperial;
using Quantities.Dimensions;
using Quantities.Prefixes;
using Quantities.Measures;
using Quantities.Measures.Si;
using Quantities.Measures.Core;
using Quantities.Measures.Other;
using Quantities.Measures.Builder;

namespace Quantities
{
    public interface IVelocityBuilder
    {
        Velocity Per<TTimeUnit>()
            where TTimeUnit : ISiAcceptedUnit, ITransform, ITime, new();
        Velocity Per<TTimePrefix, TTimeUnit>()
            where TTimePrefix : Prefix, new()
            where TTimeUnit : SiUnit, ITime, new();
        Velocity PerSecond() => Per<UnitPrefix, Second>();
    }
    public sealed class Velocity : IQuantity<IVelocity>, IVelocity, IEquatable<Velocity>, IFormattable
    {
        private static readonly VelocityBuilder _velocityFactory = new VelocityBuilder();
        public Double Value => Quantity.Value;
        public IVelocity Dimension => Quantity.Dimension;
        internal Quantity<IVelocity> Quantity { get; }
        private Velocity(Quantity<IVelocity> quantity) => Quantity = quantity;
        public IVelocityBuilder To<TUnit>()
            where TUnit : SiUnit, ILength, new()
        {
            return To<UnitPrefix, TUnit>();
        }
        public IVelocityBuilder To<TPrefix, TUnit>()
            where TPrefix : Prefix, new()
            where TUnit : SiUnit, ILength, new()
        {
            return new Transformer<TPrefix, TUnit>(Quantity);
        }
        public IVelocityBuilder ToImperial<TImperialLength>()
            where TImperialLength : IImperial, ILength, new()
        {
            return new Transformer<TImperialLength>(Quantity);
        }
        public static IVelocityBuilder Si<TUnit>(in Double velocity)
            where TUnit : SiUnit, ILength, new()
        {
            return Si<UnitPrefix, TUnit>(in velocity);
        }
        public static IVelocityBuilder Si<TPrefix, TUnit>(in Double velocity)
            where TPrefix : Prefix, new()
            where TUnit : SiUnit, ILength, new()
        {
            return new Builder<TPrefix, TUnit>(in velocity);
        }
        public static IVelocityBuilder Imperial<TUnit>(in Double velocity)
            where TUnit : IImperial, ILength, new()
        {
            return new Builder<TUnit>(in velocity);
        }
        public static Velocity operator +(Velocity left, Velocity right)
        {
            return new Velocity(left.Quantity.Add(right.Quantity));
        }
        public static Velocity operator -(Velocity left, Velocity right)
        {
            return new Velocity(left.Quantity.Subtract(right.Quantity));
        }

        public override String ToString() => Quantity.ToString();

        public String ToString(String format, IFormatProvider formatProvider) => Quantity.ToString(format, formatProvider);

        public Boolean Equals(Velocity other) => Quantity.Equals(other.Quantity);
        internal static Velocity Create(Length length, Time time)
        {
            var builder = new CompoundBuilder<ILength, ITime, IVelocity>(_velocityFactory);
            return new Velocity(builder.Build(length.Quantity, time.Quantity));
        }
        private sealed class VelocityBuilder : ICompoundFactory<ILength, ITime, IVelocity>
        {
            Quantity<IVelocity> ICompoundFactory<ILength, ITime, IVelocity>.CreateOther<TOtherA, TOtherB>(in Double a, in Double b)
            {
                return Quantity<IVelocity>.Other<VelocityOf<TOtherA, TOtherB>>(a / b);
            }
            Quantity<IVelocity> ICompoundFactory<ILength, ITime, IVelocity>.CreateOtherSi<TOtherA, TSiB>(in Double a, in Double b)
            {
                return Quantity<IVelocity>.Other<VelocityOfSi<TOtherA, TSiB>>(a / b);
            }
            Quantity<IVelocity> ICompoundFactory<ILength, ITime, IVelocity>.CreateSi<TSiA, TSiB>(in Double a, in Double b)
            {
                return Quantity<IVelocity>.Si<Velocity<TSiA, TSiB>>(a / b);
            }
            Quantity<IVelocity> ICompoundFactory<ILength, ITime, IVelocity>.CreateSiOther<TSiA, TOtherB>(in Double a, in Double b)
            {
                return Quantity<IVelocity>.Other<SiVelocityOf<TSiA, TOtherB>>(a / b);
            }
        }
        private sealed class Builder<TLengthPrefix, TLengthUnit> : IVelocityBuilder
            where TLengthPrefix : Prefix, new()
            where TLengthUnit : SiUnit, ILength, new()
        {
            private readonly Double _velocity;
            public Builder(in Double velocity) => _velocity = velocity;
            Velocity IVelocityBuilder.Per<TTimePrefix, TTimeUnit>()
            {
                return new Velocity(Quantity<IVelocity>.Si<Velocity<Length<TLengthPrefix, TLengthUnit>, Time<TTimePrefix, TTimeUnit>>>(_velocity));
            }
            Velocity IVelocityBuilder.Per<TTimeUnit>()
            {
                return new Velocity(Quantity<IVelocity>.Other<SiVelocityOf<Length<TLengthPrefix, TLengthUnit>, TTimeUnit>>(_velocity));
            }
        }
        private sealed class Transformer<TLengthPrefix, TLengthUnit> : IVelocityBuilder
            where TLengthPrefix : Prefix, new()
            where TLengthUnit : SiUnit, ILength, new()
        {
            private readonly Quantity<IVelocity> _self;
            public Transformer(Quantity<IVelocity> self) => _self = self;
            Velocity IVelocityBuilder.Per<TTimePrefix, TTimeUnit>()
            {
                return new Velocity(_self.To<Velocity<Length<TLengthPrefix, TLengthUnit>, Time<TTimePrefix, TTimeUnit>>>());
            }

            Velocity IVelocityBuilder.Per<TTimeUnit>()
            {
                return new Velocity(_self.ToOther<SiVelocityOf<Length<TLengthPrefix, TLengthUnit>, TTimeUnit>>());
            }
        }
        private sealed class Transformer<TImperialLength> : IVelocityBuilder
            where TImperialLength : IImperial, ILength, new()
        {
            private readonly Quantity<IVelocity> _self;
            public Transformer(Quantity<IVelocity> self) => _self = self;

            Velocity IVelocityBuilder.Per<TTimeUnit>()
            {
                return new Velocity(_self.ToOther<VelocityOf<TImperialLength, TTimeUnit>>());
            }
            Velocity IVelocityBuilder.Per<TTimePrefix, TTimeUnit>()
            {
                return new Velocity(_self.ToOther<VelocityOfSi<TImperialLength, Time<TTimePrefix, TTimeUnit>>>());
            }
        }
        private sealed class Builder<TImperialUnit> : IVelocityBuilder
            where TImperialUnit : IImperial, ILength, new()
        {
            private readonly Double _velocity;
            public Builder(in Double velocity) => _velocity = velocity;
            Velocity IVelocityBuilder.Per<TTimePrefix, TTimeUnit>()
            {
                return new Velocity(Quantity<IVelocity>.Other<VelocityOfSi<TImperialUnit, Time<TTimePrefix, TTimeUnit>>>(_velocity));
            }
            Velocity IVelocityBuilder.Per<TTimeUnit>()
            {
                return new Velocity(Quantity<IVelocity>.Other<VelocityOf<TImperialUnit, TTimeUnit>>(_velocity));
            }
        }
    }
}