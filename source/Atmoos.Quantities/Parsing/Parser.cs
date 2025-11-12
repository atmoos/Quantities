using Atmoos.Quantities.Serialization;

namespace Atmoos.Quantities.Parsing;

internal sealed class Parser(ISystems systems)
{
    private const Char division = '/';
    private const Char multiplication = '\u200C';
    public IEnumerable<QuantityModel> Parse(String units)
    {
        // ToDo: Add support for more than two terms (& consider support of parentheses for grouping)
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

    private QuantityModel? ParseScalar(String unit, Int32 outerExponent)
    {
        const String unknown = nameof(unknown);
        (var innerExp, unit) = Exponent(unit, systems.Exponents);
        var (system, parsedUnit) = Unit(unit, systems.Units);
        if (system is unknown) {
            return null;
        }

        unit = unit[..^parsedUnit.Length];
        var prefix = Prefix(unit, systems.Prefixes);
        var rest = prefix is null ? unit : unit[..^prefix.Length];
        if (rest.Length > 0) {
            return null;
        }

        return new(system, innerExp * outerExponent, prefix, parsedUnit);

        static (String system, String unit) Unit(String unit, IEnumerable<(String, IEnumerable<String>)> units)
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

        static (Int32 exp, String rest) Exponent(String unit, IEnumerable<(String, Int32)> exponents)
        {
            foreach (var (expStr, exp) in exponents) {
                if (unit.EndsWith(expStr)) {
                    return (exp, unit[..^expStr.Length]);
                }
            }
            return (1, unit);
        }

        static String? Prefix(String unit, IEnumerable<String> prefixes) => prefixes.FirstOrDefault(unit.StartsWith);
    }
}
