using Quantities.Measures.Si;
namespace Quantities.Measures;

internal interface IKernel
{
    static abstract Double To<TSi>(in Double value)
        where TSi : ISi;
    static abstract Double ToOther<TNonSi>(in Double value)
        where TNonSi : ITransform;

    static abstract Double Map<TKernel>(in Double value)
        where TKernel : IKernel;
}

public readonly struct SiKernel<TSiSelf> : IKernel, IRepresentable
    where TSiSelf : ISi
{
    public static Double To<TSi>(in Double value)
        where TSi : ISi
    {
        var siValue = TSiSelf.Normalise(in value);
        return TSi.Renormalise(in siValue);
    }
    public static Double ToOther<TNonSi>(in Double value)
        where TNonSi : ITransform
    {
        var normalizedSiValue = TSiSelf.Normalise(in value);
        return TNonSi.FromSi(in normalizedSiValue);
    }
    static Double IKernel.Map<TKernel>(in Double value) => TKernel.To<TSiSelf>(in value);
    public static String Representation => TSiSelf.Representation;
}

public readonly struct OtherKernel<TNonSiSelf> : IKernel, IRepresentable
    where TNonSiSelf : ITransform, IRepresentable
{
    public static Double To<TSi>(in Double value)
        where TSi : ISi
    {
        Double siValue = TNonSiSelf.ToSi(in value);
        return TSi.Renormalise(in siValue);
    }
    public static Double ToOther<TNonSi>(in Double value)
        where TNonSi : ITransform
    {
        Double siValue = TNonSiSelf.ToSi(value);
        return TNonSi.FromSi(in siValue);
    }
    static Double IKernel.Map<TKernel>(in Double value) => TKernel.ToOther<TNonSiSelf>(in value);
    public static String Representation => TNonSiSelf.Representation;
}
