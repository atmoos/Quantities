using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct Pint : IImperial, IVolume<ILength>, IUnitInject<ILength>
{
    private static readonly Transform transform = new(0.56826125e-3 /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static T Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        // 1 Pint is ~34.677429099 cubic inches
        return inject.Inject<Inch>(0.56826125e-3 / (0.0254 * 0.0254 * 0.0254) * self);
    }
    public static String Representation => "pt";
}
