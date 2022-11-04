using System.Globalization;

namespace Quantities.Measures;
public abstract class Quant : /*IEquatable<Quant>,*/ IFormattable
{
    private protected delegate Double Math(Double left, Double right);
    public Double Value { get; }
    private protected Quant(in Double value) => Value = value;
    public Quant Add(Quant right) => Compute(right, (l, r) => l + r);
    public Quant Subtract(Quant right) => Compute(right, (l, r) => l - r);
    private protected abstract Double ValueOf<TKernel>() where TKernel : IKernel;
    private protected abstract Quant Compute(Quant right, Math operation);

    public override String ToString() => ToString("g5", CultureInfo.CurrentCulture);

    public abstract String ToString(String? format, IFormatProvider? formatProvider);
    internal static Quant Create<TKernel>(in Double value)
    where TKernel : IKernel, IRepresentable
    {
        return new Impl<TKernel>(in value);
    }
    internal sealed class Impl<TKernel> : Quant
        where TKernel : IKernel, IRepresentable
    {
        public Impl(in Double value) : base(in value) { }
        private protected override Double ValueOf<TOtherKernel>() => TKernel.Map<TOtherKernel>(Value);
        private protected override Quant Compute(Quant other, Math operation)
        {
            var right = other.ValueOf<TKernel>();
            return new Impl<TKernel>(operation(Value, right));
        }

        public override String ToString(String? format, IFormatProvider? formatProvider)
        {
            return $"{Value.ToString(format, formatProvider)} {TKernel.Representation}";
        }
    }
}