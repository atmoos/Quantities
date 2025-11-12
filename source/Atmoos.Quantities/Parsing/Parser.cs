using Atmoos.Quantities.Common;
using Atmoos.Quantities.Dimensions;
using Atmoos.Quantities.Prefixes;
using Atmoos.Quantities.Serialization;
using Atmoos.Quantities.Units;
using static Atmoos.Quantities.Common.Reflection;

namespace Atmoos.Quantities.Parsing;

internal static class Parser
{
    private const Char division = '/';
    private const Char multiplication = '\u200C';
    private static readonly List<String> prefixes = Prefixes();
    private static readonly Dictionary<String, Int32> exponents = Enumerable.Range(-9, 19).Where(e => e != 1).ToDictionary(Tools.ExpToString, i => i);
    private static readonly Dictionary<String, List<String>> units = Units(); // <System, Unit[]>

    public static IEnumerable<QuantityModel> Parse(String units)
    {
        var divisionIndex = units.IndexOf(division);
        if (divisionIndex > 0) {
            var nominator = units[..divisionIndex];
            var denominator = units[(divisionIndex + 1)..];
            return Combine(ParseScalar(nominator, 1), ParseScalar(denominator, -1));
        }

        var multiplicationIndex = units.IndexOf(multiplication);
        if (multiplicationIndex > 0) {
            var leftTerm = units[..multiplicationIndex];
            var rightTerm = units[(multiplicationIndex + 1)..];
            return Combine(ParseScalar(leftTerm, 1), ParseScalar(rightTerm, 1));
        }

        return ParseScalar(units, 1) is QuantityModel scalar ? [scalar] : [];

        static IEnumerable<QuantityModel> Combine(QuantityModel? left, QuantityModel? right)
            => left is QuantityModel l && right is QuantityModel r ? [l, r] : [];
    }

    private static QuantityModel? ParseScalar(String unit, Int32 outerExponent)
    {
        const String unknown = nameof(unknown);
        (var innerExp, unit) = Exponent(unit);
        var (system, parsedUnit) = Unit(unit);
        if (system is unknown) {
            return null;
        }

        unit = unit[..^parsedUnit.Length];
        var prefix = Prefix(unit);
        var rest = prefix is null ? unit : unit[..^prefix.Length];
        if (rest.Length > 0) {
            return null;
        }

        return new(system, innerExp * outerExponent, prefix, parsedUnit);

        static (String system, String unit) Unit(String unit)
        {
            foreach (var (system, candidateUnits) in units) {
                foreach (var candidateUnit in candidateUnits) {
                    if (unit.EndsWith(candidateUnit)) {
                        return (system, candidateUnit);
                    }
                }
            }
            return (unknown, String.Empty);
        }

        static (Int32 exp, String rest) Exponent(String unit)
        {
            foreach (var (expStr, exp) in exponents) {
                if (unit.EndsWith(expStr)) {
                    return (exp, unit[..^expStr.Length]);
                }
            }
            return (1, unit);
        }

        static String? Prefix(String unit) => prefixes.FirstOrDefault(unit.StartsWith);
    }

    private static Dictionary<String, List<String>> Units()
    {
        Type si = typeof(ISiUnit);
        Type metric = typeof(IMetricUnit);
        Type imperial = typeof(IImperialUnit);
        Type nonStandard = typeof(INonStandardUnit);
        var allTypes = typeof(Parser).Assembly.GetExportedTypes();
        return new() {
            [nameof(si)] = [.. allTypes.Where(t => t.Implements(si)).Select(GetRepresentation)],
            [nameof(metric)] = [.. allTypes.Where(t => t.Implements(metric)).Select(GetRepresentation)],
            [nameof(imperial)] = [.. allTypes.Where(t => t.Implements(imperial)).Select(GetRepresentation)],
            ["any"] = [.. allTypes.Where(t => t.Implements(nonStandard)).Select(GetRepresentation)],
        };
    }

    private static List<String> Prefixes()
    {
        Type metric = typeof(IMetricPrefix);
        Type binary = typeof(IBinaryPrefix);
        var allTypes = typeof(Parser).Assembly.GetExportedTypes();
        IEnumerable<Type> metricPrefixes = allTypes.Where(t => t.Implements(metric));
        return [.. metricPrefixes.Concat(allTypes.Where(t => t.Implements(binary))).Select(GetRepresentation)];
    }
}
