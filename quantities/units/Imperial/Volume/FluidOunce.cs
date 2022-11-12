using Quantities.Dimensions;
using Quantities.Unit.Imperial.Length;
using Quantities.Unit.Transformation;

namespace Quantities.Unit.Imperial.Volume;

public readonly struct FluidOunce : IImperial, IVolume<ILength>, IUnitInject<ILength>
{
    private static readonly Transform transform = new(0.0284130625e-3 /* m³ */);
    public static Double ToSi(in Double nonSiValue) => transform.ToSi(in nonSiValue);
    public static Double FromSi(in Double siValue) => transform.FromSi(in siValue);
    public static T Inject<T>(in IInject<ILength, T> inject, in Double self)
    {
        // 1 fluid ounce is ~1.733871 cubic inch
        return inject.Inject<Inch>(0.0284130625e-3 / (0.0254 * 0.0254 * 0.0254) * self);
    }

    public static String Representation => "fl oz";
}
