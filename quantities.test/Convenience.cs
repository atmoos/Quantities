using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quantities.Quantities;

namespace Quantities.Test;
public static class Convenience
{
    public static Int32 SiPrecision => 15;
    public static Int32 ImperialPrecision => 14;
    public static void Matches(this Length actual, Length expected)
    {
        actual.Matches(expected, SiPrecision);
    }
    public static void Matches(this Length actual, Length expected, Int32 precision)
    {
        Assert.Equal((Double)expected, (Double)actual, precision);
    }
}