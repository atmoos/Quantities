namespace Quantities.Dimensions;

internal static class Tools
{
    public static String ExpToString(Int32 exp)
    {
        var sign = exp >= 0 ? String.Empty : "⁻";
        var value = exp switch {
            1 => String.Empty,
            _ => Sup(Math.Abs(exp))
        };
        return $"{sign}{value}";

        static String Sup(Int32 n) => n switch {
            1 => "\u00B9",
            2 or 3 => ((Char)(0x00B0 + n)).ToString(),
            var m when m is >= 0 and < 10 => ((Char)(0x2070 + m)).ToString(),
            _ => Build(n)
        };
        static String Build(Int32 n)
        {
            var digits = new Stack<String>();
            while (n >= 10) {
                (n, var digit) = Int32.DivRem(n, 10);
                digits.Push(Sup(digit));
            }
            digits.Push(Sup(n));
            return String.Join(String.Empty, digits);
        }
    }
}
